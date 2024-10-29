using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Movie.Entities;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Movie;

public class FormatRepository(MyDbContext _context) : IFormatRepository
{
    public async Task<Format?> GetById(long id)
    {
        return await _context.Formats.FindAsync(id);
    }
    
    public async Task<List<Format>> GetAll()
    {
        return await _context.Formats.ToListAsync();
    }
}