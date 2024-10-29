using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Cinema;

public class SeatTypeRepository : ISeatTypeRepository
{
    private readonly MyDbContext _context;

    public SeatTypeRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<SeatType>> GetAll()
    {
        return await _context.SeatTypes.ToListAsync(); 
    }
}