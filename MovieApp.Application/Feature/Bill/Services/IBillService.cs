using System.Security.Claims;
using MovieApp.Application.Feature.Bill.Dtos;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Show.Entities;

namespace MovieApp.Application.Feature.Bill.Services;

public interface IBillService
{
    Task<string> CreateBill(BillCreate billCreate, ClaimsPrincipal claimsPrincipal);

    void CheckSeatInHall(List<long> seatIds, Hall hall);

    Task CheckSeatAreReserved(List<long> seatIds, string showId);

    ICollection<Ticket> CreateTicket(Domain.Show.Entities.Show show, List<Seat> seats, Domain.Bill.Entities.Bill bill);

    Task<string> Payment(string billId, string responseCode, string transactionStatus, string paymentAt);
}