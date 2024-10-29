using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Show.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Show;

public class ShowtimeRepository(MyDbContext context) : IShowtimeRepository
{
    public async Task<List<Domain.Show.Entities.Show>> FindByStartDateAndHallId(DateOnly starDate, string id)
    {
        return await context.Shows.Where(s => s.StartDate >= starDate && s.Id == id).ToListAsync();
    }

    public async Task<List<Domain.Show.Entities.Show>> Save(List<Domain.Show.Entities.Show> shows)
    {
        foreach (var show in shows) await context.Shows.AddAsync(show);
        await context.SaveChangesAsync();
        return shows;
    }

    public async Task<Domain.Show.Entities.Show?> GetShowByIdCheckDateTime(string id)
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
            .ThenInclude(h => h.Seats
                .OrderBy(s => s.RowName)
                .ThenBy(s => s.RowIndex)
            )
            .ThenInclude(s => s.Type)
            .Include(s => s.Format)
            .FirstOrDefaultAsync();
    }
}