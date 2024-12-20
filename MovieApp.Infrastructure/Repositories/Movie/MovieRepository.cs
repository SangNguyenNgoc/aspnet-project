﻿using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Movie;

public class MovieRepository(MyDbContext context) : IMovieRepository
{
    public async Task<Domain.Movie.Entities.Movie?> GetMovieById(string id)
    {
        return await context.Movies
            .Include(m => m.Formats)
            .Include(m => m.Genres)
            .Include(m => m.Status)
            .SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Domain.Movie.Entities.Movie>>? GetAllMoviesByDate(DateOnly date)
    {
        // Truy vấn tất cả các bộ phim có ngày phát hành trước 'date' và ngày kết thúc sau 'date'
        return await context.Movies
            .Where(m => m.ReleaseDate < date && m.EndDate > date)
            .Include(m => m.Formats)
            .ToListAsync();
    }

    public async Task<Domain.Movie.Entities.Movie?> GetMovieBySlug(string slug)
    {
        return await context.Movies
            .Where(m => m.Slug == slug)
            .Include(m => m.Formats)
            .Include(m => m.Genres)
            .Include(m => m.Status)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Domain.Movie.Entities.Movie>> GetMovieByStatusAndOrderByRating(string slug)
    {
        return await context.Movies
            .Where(m => m.Status.Slug == slug)
            .OrderByDescending(m => m.SumOfRatings)
            .ThenBy(m => m.ReleaseDate)
            .ToListAsync();
    }
    
    public async Task<List<Domain.Movie.Entities.Movie>> GetMovieAndShowFor7Date()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var endDate = startDate.AddDays(7);
        return await context.Movies
            .Include(m => m.Shows.Where(s => s.StartDate>= startDate && s.StartDate <= endDate))
            .ThenInclude(s => s.Format)          
            .Include(m => m.Shows)
            .ThenInclude(s => s.Hall)
            .ThenInclude(h => h.Cinema)
            .Where(m => m.Shows.Any(s => s.StartDate >= startDate && s.StartDate <= endDate))
            .ToListAsync();
    }

    public async Task<string> Save(Domain.Movie.Entities.Movie movie)
    {
        var newMovie = await context.Movies.AddAsync(movie);
        await context.SaveChangesAsync();
        return newMovie.Entity.Id;
    }

    public Task<List<Domain.Movie.Entities.Movie>> GetAll()
    {
        return context.Movies
            .Include(m => m.Status)
            .Include(m => m.Formats)
            .ToListAsync();
    }

    public async Task<List<Domain.Movie.Entities.Movie>?> GetMovieByYear(int year)
    {
        return await context.Movies
            .Where(m => m.Shows.Any(s => s.StartDate.Year == year))
            .Include(m => m.Shows
                .Where(s => s.StartDate.Year == year))
            .ThenInclude(s => s.Tickets)
            .ThenInclude(t => t.Seat)
            .ThenInclude(s => s.Type)
            .ToListAsync();
    }

    public async Task<Domain.Movie.Entities.Movie?> GetBestMovie(DateTime from, DateTime to)
    {
        var fromDate = DateOnly.FromDateTime(from);
        var toDate = DateOnly.FromDateTime(to);
        return await context.Movies
            .Where(m => m.Shows.Any(s => s.StartDate >= fromDate && s.StartDate <= toDate))
            .Include(m => m.Status)
            .OrderByDescending(m => m.Shows
                .Where(s => s.StartDate >= fromDate && s.StartDate <= toDate)
                .Sum(s => s.Tickets.Sum(t => t.Seat.Type.Price)))
            .FirstOrDefaultAsync();
    }

    public async Task<string> Update(Domain.Movie.Entities.Movie movie)
    {
        var newMovie = context.Movies.Update(movie);
        await context.SaveChangesAsync();
        return "success";
    }
}