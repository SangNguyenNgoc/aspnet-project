using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Cinema;

public class CinemaStatusRepository : ICinemaStatusRepository
{
    private readonly MyDbContext _context;

    public CinemaStatusRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CinemaStatus?>> GetAll()
    {
        return await _context.CinemaStatus.ToListAsync();
    }
    
    public async Task<CinemaStatus?> GetById(long id)
    {
        return await _context.CinemaStatus.FindAsync(id);
    }
    
    public async Task<CinemaStatus?> GetDifferentId(long id)
    {
        return await _context.CinemaStatus.FirstOrDefaultAsync(c => c.Id != id);
    }
}