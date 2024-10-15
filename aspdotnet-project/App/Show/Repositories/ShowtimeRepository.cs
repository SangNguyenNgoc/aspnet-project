using aspdotnet_project.Context;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.Show.Repositories;

public class ShowtimeRepository(MyDbContext context) : IShowtimeRepository
{
    public async Task<List<Entities.Show>> FindByStartDateAndHallId(DateOnly starDate, string id)
    {
        return await context.Shows.Where(s => s.StartDate >= starDate && s.Id == id).ToListAsync();
    }

    public async Task<List<Entities.Show>> Save(List<Entities.Show> shows)
    {
        foreach (var show in shows) await context.Shows.AddAsync(show);
        await context.SaveChangesAsync();
        return shows;
    }

    public async Task<Entities.Show?> GetShowByIdCheckDateTime(string id)
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var startTime = TimeOnly.FromDateTime(DateTime.Now);

        return await context.Shows
            .Where(s => s.Id == id &&
                        (s.StartDate > startDate || (s.StartTime >= startTime && s.StartDate == startDate)))
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .ThenInclude(h => h.Cinema)
            .Include(s => s.Hall)
            .ThenInclude(h => h.Seats.OrderBy(s => s.Order))
            .Include(s => s.Format)
            .FirstOrDefaultAsync();
    }
}