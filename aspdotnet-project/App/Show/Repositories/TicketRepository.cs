using aspdotnet_project.App.Show.Entities;
using aspdotnet_project.Context;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.Show.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly MyDbContext _context;

    public TicketRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Ticket>> GetTicketsByShowId(string showId)
    {
        return await _context.Tickets
            .Include(t => t.Show)
            .Include(t=> t.Seat)
            .Where(t => t.Show.Id == showId 
                        && ( (t.Bill.Status.Id == 1 && t.Bill.ExpireAt > DateTime.Now) 
                             || t.Bill.Status.Id == 2)
            )
            .OrderBy(t => t.Seat.Order)
            .ToListAsync();
    }
}