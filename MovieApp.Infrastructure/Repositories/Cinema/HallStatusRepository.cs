using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Cinema;

public class HallStatusRepository : IHallStatusRepository
{
    private readonly MyDbContext _context;

    public HallStatusRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<HallStatus>> GetAllHallStatus()
    {
        return await _context.HallStatus.ToListAsync();
    }

    public async Task<HallStatus?> GetById(long id)
    {
        return await _context.HallStatus.FindAsync(id);
    }
}