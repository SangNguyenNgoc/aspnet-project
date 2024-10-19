using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Show.Entities;
using MovieApp.Domain.Show.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Show;

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