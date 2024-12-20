﻿using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Cinema;

public class HallRepository : IHallRepository
{
    private readonly MyDbContext _context;

    public HallRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hall>?> GetHallsByDate(DateOnly date, string cinemaId)
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
    
    public async Task<long> Save(Hall hall)
    {
        var newHall = await _context.Halls.AddAsync(hall);
        await _context.SaveChangesAsync();
        return newHall.Entity.Id;
    }
    
    public async Task<Hall?> GetHallById(long hallId)
    {
        return await _context.Halls
            .Include(h => h.Seats)
            .ThenInclude(s => s.Type)
            .Include(h => h.Cinema)
            .FirstOrDefaultAsync(h => h.Id == hallId);
    }

    public async Task<Hall> UpdateStatus(Hall hall)
    {
        var hallUpdate = _context.Halls.Update(hall).Entity;
        await _context.SaveChangesAsync();
        return hallUpdate;
    }
}