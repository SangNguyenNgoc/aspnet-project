using MovieApp.Domain.Movie.Entities;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Movie;

public class FormatRepository(MyDbContext _context) : IFormatRepository
{
    public async Task<Format?> GetMovieById(long id)
    {
        return await _context.Formats.FindAsync(id);
    }
}