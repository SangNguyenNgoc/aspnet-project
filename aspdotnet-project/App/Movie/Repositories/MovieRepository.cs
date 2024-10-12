using aspdotnet_project.Context;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.Movie.Repositories;

public class MovieRepository(MyDbContext context) : IMovieRepository
{
    private readonly MyDbContext _context = context;
    public async Task<Entities.Movie> GetMovieById(string id)
    {
        return (await _context.Movies.FindAsync(id))!;
    }

    public async Task<List<Entities.Movie>> GetAllMovies(DateOnly date)
    {
        // Truy vấn tất cả các bộ phim có ngày phát hành trước 'date' và ngày kết thúc sau 'date'
        return await _context.Movies
            .Where(m => m.ReleaseDate < date && m.EndDate > date)
            .Include(m => m.Formats)
            .ToListAsync();
    }

    public async Task<Entities.Movie?> GetMovieBySlug(string slug)
    {
        return await _context.Movies
            .Where(m => m.Slug == slug)
            .Include(m => m.Formats)
            .Include(m => m.Genres)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Entities.Movie>> GetMovieByStatusAndOrderByRating(string slug)
    {
        return await _context.Movies
            .Where(m => m.Status.Slug == slug)
            .OrderByDescending(m => m.SumOfRatings)
            .ThenBy(m => m.ReleaseDate)
            .ToListAsync();
    }
    
    public async Task<List<Entities.Movie>> GetMovieAndShowByCinemaId()
    {
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly endDate = startDate.AddDays(7);
        return await _context.Movies
            .Include(m => m.Shows.Where(s => s.StartDate>= startDate && s.StartDate <= endDate))
            .ThenInclude(s => s.Format)          
            .Include(m => m.Shows)
            .ThenInclude(s => s.Hall)
            .ThenInclude(h => h.Cinema)
            .Where(m => m.Shows.Any(s => s.StartDate >= startDate && s.StartDate <= endDate))
            .ToListAsync();
    }
}