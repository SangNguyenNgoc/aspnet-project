using aspdotnet_project.App.Bill.Dtos;
using aspdotnet_project.App.Cinema.Entities;
using aspdotnet_project.App.Show.Entities;

namespace aspdotnet_project.App.Bill.Services;

public interface IBillService
{
    Task<string> CreateBill(BillCreate billCreate);

    Task CheckSeatInHall(List<long> seatIds, Hall hall);

    Task CheckSeatAreReserved(List<long> seatIds, string showId);

    ICollection<Ticket> CreateTicket(Show.Entities.Show show, List<Seat> seats, string showId);

    Task<string> Payment(string billId, string responseCode, string transactionStatus, string paymentAt);

    Task<string> GeneratePaymentUrl(long cost, string id);
}