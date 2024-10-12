using aspdotnet_project.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace aspdotnet_project.App.Show.Repositories;

public class ShowtimeRepository(MyDbContext context) : IShowtimeRepository
{
    private readonly MyDbContext _context = context;
    
    public async Task<List<Entities.Show>> FindByStartDateAndHallId(DateOnly starDate, string id)
    {
        return await _context.Shows.Where(s=>s.StartDate>=starDate && s.Id == id).ToListAsync();
    }

    public async Task<List<Entities.Show>> Save(List<Entities.Show> shows)
    {
        foreach (var show in shows)
        {
            await _context.Shows.AddAsync(show);
        }
        await _context.SaveChangesAsync();
        return shows;
    }
    
    public async Task<Entities.Show?> GetShowByIdCheckDateTime(string id)
    {
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
        TimeOnly startTime = TimeOnly.FromDateTime(DateTime.Now);

        return await _context.Shows
            .Where(s => s.Id == id && (s.StartDate > startDate || (s.StartTime >= startTime && s.StartDate == startDate) ))
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .ThenInclude(h => h.Cinema)
            .Include(s => s.Hall)
            .ThenInclude(h => h.Seats.OrderBy(s => s.Order))
            .Include(s => s.Format)
            .FirstOrDefaultAsync();
    }
}