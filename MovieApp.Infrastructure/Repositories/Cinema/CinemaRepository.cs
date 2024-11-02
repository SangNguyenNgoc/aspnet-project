using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Cinema;

public class CinemaRepository : ICinemaRepository
{
    private readonly MyDbContext _context;

    public CinemaRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Domain.Cinema.Entities.Cinema>> GetCinemasByStatusOrderByCreateDate(string slug, DateOnly startDate, DateOnly endDate)
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

    public async Task<List<Domain.Cinema.Entities.Cinema>> GetCinemaAdmin()
    {
        return await _context.Cinemas
            .Include(c => c.Location)
            .ToListAsync();
    }

    public async Task<string> Save(Domain.Cinema.Entities.Cinema cinema)
    {
        var newCinema = await _context.Cinemas.AddAsync(cinema);
        await _context.SaveChangesAsync();
        return newCinema.Entity.Id;
    }

    public async Task<Domain.Cinema.Entities.Cinema?> GetById(string id)
    {
        return await _context.Cinemas.FindAsync(id);
    }
    
    public async Task<Domain.Cinema.Entities.Cinema?> GetDetailById(string id)
    {
        return await _context.Cinemas
            .Include(c => c.Halls)
            .ThenInclude(h => h.Status)
            .Include(c => c.Location)
            .Include(c => c.Status)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}