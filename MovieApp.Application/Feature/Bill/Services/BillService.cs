using System.Globalization;
using System.Security.Claims;
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


    public BillService(IBillRepository billRepository, IBillStatusRepository billStatusRepository,
        IShowtimeRepository showtimeRepository, ISeatRepository seatRepository, ITicketRepository ticketRepository, 
        IUserRepository userRepository, VnPayService vnPayService)
    {
        _billRepository = billRepository;
        _billStatusRepository = billStatusRepository; 
        _showtimeRepository = showtimeRepository;
        _seatRepository = seatRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _vnPayService = vnPayService;
    }

    public async Task<string> CreateBill(BillCreate billCreate, ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) 
                     ?? throw new UnauthorizedAccessException("Unauthorized");
        
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
            ExpireAt = DateTime.Now.AddMinutes(_vnPayService.getTimeOut()),
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
            return "Success";
        }
        var message = _vnPayService.GetMessage(responseCode, transactionStatus);
        bill.FailureReason = message;
        bill.FailureAt = dateTime;
        return message;
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
    
    
}