using aspdotnet_project.App.Cinema.Entities;
using aspdotnet_project.Context;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.Cinema.Repositories;

public class HallRepository : IHallRepository
{
    private readonly MyDbContext _context;

    public HallRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hall>> GetHallsByDate(DateOnly date, string cinemaId)
    {
        var halls = await _context.Halls
            .Where(h => h.Cinema.Id == cinemaId).Include(hall => hall.Shows)
            .ToListAsync();

        // Kiểm tra xem có bất kỳ hội trường nào có show hay không
        var hasShows = halls.Any(h => h.Shows.Any(s=> s.StartDate == date));

        // Nếu có hội trường nào có show thì trả về null
        return hasShows ? null! :
            // Nếu không có hội trường nào có show thì trả về danh sách hội trường
            halls;
    }
}