using aspdotnet_project.App.Show.Entities;

namespace aspdotnet_project.App.Show.Repositories;

public interface ITicketRepository
{
    Task<List<Ticket>> getTicketsByShowId(string showId);
}