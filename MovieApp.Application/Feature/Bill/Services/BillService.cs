using System.Globalization;
using AutoMapper;
using MovieApp.Application.Exception;
using MovieApp.Application.Feature.Bill.Dtos;
using MovieApp.Domain.Bill.Repositories;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Domain.Show.Entities;
using MovieApp.Domain.Show.Repositories;
using MovieApp.Domain.User.Repositories;
using MovieApp.Infrastructure.VnPay;

namespace MovieApp.Application.Feature.Bill.Services;

public class BillService : IBillService
{
    private readonly IBillRepository _billRepository;
    private readonly IBillStatusRepository _billStatusRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IShowtimeRepository _showtimeRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;

    private readonly VnPayService _vnPayService;
    private readonly IMapper _mapper;


    public BillService(IBillRepository billRepository, IBillStatusRepository billStatusRepository,
        IShowtimeRepository showtimeRepository, ISeatRepository seatRepository, ITicketRepository ticketRepository, 
        IUserRepository userRepository, VnPayService vnPayService, IMapper mapper)
    {
        _billRepository = billRepository;
        _billStatusRepository = billStatusRepository; 
        _showtimeRepository = showtimeRepository;
        _seatRepository = seatRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _vnPayService = vnPayService;
        _mapper = mapper;
    }

    
    public async Task<string> CreateBill(BillCreate billCreate, string userId)
    {
        var user = await _userRepository.GetUserById(userId) ?? throw new DataNotFoundException("User not found");
        
        var show = await _showtimeRepository.GetShowByIdCheckDateTime(billCreate.ShowId) 
                   ?? throw new DataNotFoundException("Show not found");
        
        CheckSeatInHall(billCreate.SeatIds, show.Hall);
        await CheckSeatAreReserved(billCreate.SeatIds, billCreate.ShowId);

        var seats = await _seatRepository.GetAllById(billCreate.SeatIds);
        var totalPrice = seats.Sum(seat => seat.Type.Price);

        var billStatus = await _billStatusRepository.GetBillStatusById(1) ??
                         throw new DataNotFoundException("Bill status not found");

        var billId = DateTime.Now.Ticks.ToString();
        var paymentUrl = _vnPayService.GeneratePaymentUrl(totalPrice, billId);

        var bill = new Domain.Bill.Entities.Bill
        {
            Id = billId,
            Status = billStatus,
            User = user,
            Total = totalPrice,
            CreateAt = DateTime.Now,
            ExpireAt = DateTime.Now.AddMinutes(_vnPayService.GetTimeOut()),
            PaymentUrl = paymentUrl,
            PaymentAt = null,
            FailureAt = null,
            FailureReason = null
        };
        var tickets = CreateTicket(show, seats, bill);
        bill.Tickets = tickets;
        await _billRepository.Create(bill);
        return paymentUrl;
    }

    
    public void CheckSeatInHall(List<long> seatIds, Hall hall)
    {
        var seatIdsInHall = hall.Seats
            .Select(seat => seat.Id)
            .ToHashSet();
        if (seatIds.Any(seatId => !seatIdsInHall.Contains(seatId)))
        {
            throw new DataNotFoundException("Data not found", ["Seats are not found"]);
        }
    }
    

    public async Task CheckSeatAreReserved(List<long> seatIds, string showId)
    {
        var tickets = await _ticketRepository.GetTicketsByShowId(showId);

        var seatIdsAreReserved = tickets
            .Select(ticket => ticket.Seat.Id)
            .ToHashSet();

        if (seatIdsAreReserved.Any(seatIds.Contains))
        {
            throw new BadRequestException("Input invalid", ["Seats are reserved"]);
        }
    }

    
    public async Task<string> Payment(string billId, string responseCode, string transactionStatus, string paymentAt)
    {
        var bill = await _billRepository.GetByIdAsync(billId) ?? throw new DataNotFoundException("Bill not found");
        const string formatter = "yyyyMMddHHmmss";
        var dateTime = DateTime.ParseExact(paymentAt, formatter, CultureInfo.InvariantCulture);
        if (responseCode == "00" && transactionStatus == "00")
        {
            var newStatus = await _billStatusRepository.GetBillStatusById(2) 
                            ?? throw new DataNotFoundException("Bill status not found");
            if (!string.IsNullOrEmpty(bill.FailureReason)) bill.FailureReason = null;
            bill.Status = newStatus;
            bill.PaymentAt = dateTime;
            await _billRepository.UpdateAsync(bill);
        }
        else
        {
            var message = _vnPayService.GetMessage(responseCode, transactionStatus);
            bill.FailureReason = message;
            bill.FailureAt = dateTime;
        }
        return _vnPayService.GetBillDetailUrl() + "?id=" + bill.Id;
    }
    
    
    public ICollection<Ticket> CreateTicket(Domain.Show.Entities.Show show, List<Seat> seats, 
        Domain.Bill.Entities.Bill bill)
    {
        return seats.Select(seat => new Ticket
        {
            Id = Guid.NewGuid().ToString(),
            Bill = bill,
            Show = show,
            Seat = seat,
        }).ToHashSet();
    }

    
    public async Task<ICollection<BillDetail>> GetBillsByUser(string userId)
    {
        var bills = await _billRepository.GetByUserIdAsync(userId);
        var mapBillAndTicket =
            await _ticketRepository.GetAllTicketByBillIdGroupByBillId(bills.Select(bill => bill.Id).ToList());
        var billDetails = bills.Select(bill =>
        {
            mapBillAndTicket.TryGetValue(bill.Id, out var ticketDetail);
            var billDetail = MapBillDetail(bill, ticketDetail!);
            return billDetail;
        }).ToList();
        return billDetails;
    }


    private async Task<BillDetail> GetBillDetail(string billId, string? userId = null)
    {
        var bill = await _billRepository.GetBillDetailById(billId) 
                   ?? throw new DataNotFoundException("Bill not found");

        if (userId != null && bill.User.Id != userId)
        {
            throw new UnauthorizedAccessException();
        }
        var ticketDetail = await _ticketRepository.GetTicketDetailById(bill.Tickets.First().Id)
                           ?? throw new DataNotFoundException("Ticket not found");
        var billDetail = MapBillDetail(bill, ticketDetail);

        return billDetail;
    }

    
    private BillDetail MapBillDetail(Domain.Bill.Entities.Bill bill, Ticket ticketDetail)
    {
        var billDetail = _mapper.Map<BillDetail>(bill);

        billDetail.Customer = new BillDetail.UserDtoInBillDetail
        {
            Fullname = bill.User.FullName,
            Email = bill.User.Email!
        };

        billDetail.Show = new BillDetail.ShowDtoInBillDetail
        {
            Id = ticketDetail.Show.Id,
            RunningTime = ticketDetail.Show.RunningTime,
            StartDate = ticketDetail.Show.StartDate,
            StartTime = ticketDetail.Show.StartTime,
            Format = ticketDetail.Show.Format.Version + " " + ticketDetail.Show.Format.Caption
        };

        billDetail.Movie = new BillDetail.MovieDtoInBillDetail
        {
            Id = ticketDetail.Show.Movie.Id,
            Name = ticketDetail.Show.Movie.Name,
            SubName = ticketDetail.Show.Movie.SubName,
            Poster = ticketDetail.Show.Movie.Poster,
            AgeRestriction = ticketDetail.Show.Movie.AgeRestriction,
            HorizontalPoster = ticketDetail.Show.Movie.HorizontalPoster
        };

        billDetail.Cinema = new BillDetail.CinemaDtoInBillDetail
        {
            Id = ticketDetail.Show.Hall.Cinema.Id,
            Name = ticketDetail.Show.Hall.Cinema.Name,
            HallName = ticketDetail.Show.Hall.Name
        };

        return billDetail;
    }

    
    public Task<BillDetail> GetBillDetailByUser(string billId, string userId) => GetBillDetail(billId, userId);
    
    
    public Task<BillDetail> GetBillDetailByAdmin(string billId) => GetBillDetail(billId);


    public async Task UpdateBillsExpired(DateTime dateTime)
    {
        await _billRepository.UpdateExpiredBillsStatus(dateTime);
    }
}