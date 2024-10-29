using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Movie.Entities;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Movie;

public class GenreRepository(MyDbContext _context): IGenreRepository
{
    public async Task<List<Genre>> GetAll()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre?> GetById(long id)
    {
        return await _context.Genres.FindAsync(id);
    }
}