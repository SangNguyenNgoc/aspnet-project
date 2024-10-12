using aspdotnet_project.Context;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.Cinema.Repositories;

public class CinemaRepository : ICinemaRepository
{
    private readonly MyDbContext _context;

    public CinemaRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Entities.Cinema>> GetCinemasByStatusOrderByCreateDate(string slug, DateOnly startDate, DateOnly endDate)
    {
        return await _context.Cinemas
            .Include(c => c.Halls)
            .ThenInclude(h => h.Shows
                .Where(s => s.Movie.Slug == slug 
                            && s.StartDate >= startDate 
                            && s.StartDate <= endDate
                            )
            )
            .ThenInclude(s => s.Format)
            .Where(c => c.Status.Id == 1)
            .Where(c => c.Halls
                .Any(h => h.Shows
                    .Any(s => s.Movie.Slug == slug 
                              && s.StartDate >= startDate 
                              && s.StartDate <= endDate
                              )
                ))
            .ToListAsync();
    }
}