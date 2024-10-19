using MovieApp.Application.Feature.Cinema.Dtos;

namespace MovieApp.Application.Feature.Cinema.Services;

public interface ICinemaService
{
    Task<List<LocationAndCinema>> GetAllCinemas();
};