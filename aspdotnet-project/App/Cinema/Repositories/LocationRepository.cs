using aspdotnet_project.App.Cinema.Entities;
using aspdotnet_project.Context;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.Cinema.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly MyDbContext _context;

    public LocationRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Location>> getAllLocationAndCinema()
    {
        return await _context.Locations
            .Include(l => l.Cinemas)
            .ToListAsync();

    }
}