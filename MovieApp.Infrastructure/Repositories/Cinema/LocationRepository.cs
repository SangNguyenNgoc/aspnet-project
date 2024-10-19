using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Cinema;

public class LocationRepository : ILocationRepository
{
    private readonly MyDbContext _context;

    public LocationRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Location>> GetAllLocationAndCinema()
    {
        return await _context.Locations
            .Include(l => l.Cinemas)
            .ToListAsync();

    }
}