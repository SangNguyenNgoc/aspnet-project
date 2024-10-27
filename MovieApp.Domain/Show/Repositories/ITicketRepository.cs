using MovieApp.Domain.Show.Entities;

namespace MovieApp.Domain.Show.Repositories;

public interface ITicketRepository
{
    Task<List<Ticket>> GetTicketsByShowId(string showId);

    Task<Ticket?> GetTicketDetailById(string ticketId);

    Task<Dictionary<string, Ticket?>> GetAllTicketByBillIdGroupByBillId(List<string> billIds);
}