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

    public async Task<List<Domain.Cinema.Entities.Cinema>> GetCinemaByYear(int year)
    {
        return await _context.Cinemas
            .Include(c => c.Halls)
            .ThenInclude(h => h.Shows
                .Where(s => s.StartDate.Year == year)
            )
            .ThenInclude(s => s.Tickets)
            .ThenInclude(t => t.Seat)
            .ThenInclude(s => s.Type)
            .ToListAsync();
    }

    public Task<Domain.Cinema.Entities.Cinema> UpdateStatus(Domain.Cinema.Entities.Cinema cinema)
    {
        _context.Cinemas.Update(cinema);
        _context.SaveChanges();
        return Task.FromResult(cinema);
    }

    public async Task<Domain.Cinema.Entities.Cinema?> GetBestCinema(DateTime from, DateTime to)
    {
        var fromDate = DateOnly.FromDateTime(from);
        var toDate = DateOnly.FromDateTime(to);
        return await _context.Cinemas
            .Where(c => c.Halls.Any(h => h.Shows.Any(s => s.StartDate >= fromDate && s.StartDate <= toDate)))
            .Include(c => c.Location)
            .OrderByDescending(c => c.Halls.Sum(h => h.Shows
                .Where(s => s.StartDate >= fromDate && s.StartDate <= toDate)
                .Sum(s => s.Tickets.Sum(t => t.Seat.Type.Price))))
            .FirstOrDefaultAsync();

    }
}