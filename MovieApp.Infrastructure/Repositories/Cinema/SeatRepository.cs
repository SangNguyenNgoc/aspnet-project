﻿using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Cinema;

public class SeatRepository : ISeatRepository
{
    private readonly MyDbContext _context;

    public SeatRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Seat>> GetAllById(List<long> seatIds)
    {
        var seats = await _context.Seats
            .Where(seat => seatIds.Contains(seat.Id))
            .Include(seat => seat.Type)
            .ToListAsync();
        return seats;
    }
}