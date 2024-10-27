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

    public async Task<Ticket?> GetTicketDetailById(string ticketId)
    {
        return await _context.Tickets
            .Include(t => t.Show) 
                .ThenInclude(s => s.Movie)
            .Include(t => t.Show)
                .ThenInclude(s => s.Format)
            .FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    public async Task<Dictionary<string, Ticket?>> GetAllTicketByBillIdGroupByBillId(List<string> billIds)
    {
        return await _context.Tickets
            .Where(ticket => billIds.Contains(ticket.Bill.Id))
            .Include(ticket => ticket.Show)
                .ThenInclude(show => show.Movie)
            .Include(ticket => ticket.Show)
                .ThenInclude(show => show.Format)
            .GroupBy(ticket => ticket.Bill.Id)
            .ToDictionaryAsync(
                group => group.Key,             
                group => group.FirstOrDefault() 
            );
    }
}