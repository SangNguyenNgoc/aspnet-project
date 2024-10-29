using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Movie.Entities;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Movie;

public class MovieStatusRepository(MyDbContext _context) : IMovieStatusRepository
{
    public async Task<List<MovieStatus>> FindByIdOrId(long id1, long id2)
    {
        return await _context.MovieStatus
            .Include(ms => ms.Movies)
            .ThenInclude(mv => mv.Formats)
            .Include(ms => ms.Movies)
            .ThenInclude(mv => mv.Genres)
            .Where(ms => ms.Id == id1 || ms.Id == id2)
            .OrderBy(ms => ms.Id)
            .AsSplitQuery() 
            .ToListAsync();
    }

    public async Task<List<MovieStatus>> GetAll()
    {
        return await _context.MovieStatus.ToListAsync();
    }
    
    public async Task<MovieStatus?> FindById(long id)
    {
        return await _context.MovieStatus
            .FirstOrDefaultAsync(ms => ms.Id == id);
    }
}