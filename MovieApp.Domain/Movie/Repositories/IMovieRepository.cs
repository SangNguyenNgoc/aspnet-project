﻿namespace MovieApp.Domain.Movie.Repositories;

public interface IMovieRepository
{
    Task<Entities.Movie?> GetMovieById(string id);
    Task<List<Entities.Movie>> GetAllMoviesByDate(DateOnly date);
    Task<Entities.Movie?> GetMovieBySlug(string slug);
    Task<List<Entities.Movie>> GetMovieByStatusAndOrderByRating(string slug);
    Task<List<Entities.Movie>> GetMovieAndShowFor7Date();
    Task<string> Save(Domain.Movie.Entities.Movie movie);
    Task<List<Entities.Movie>> GetAll();
    Task<List<Entities.Movie>?> GetMovieByYear(int year);
    Task<Entities.Movie?> GetBestMovie(DateTime from, DateTime to);  
    Task<string> Update(Domain.Movie.Entities.Movie movie);
}