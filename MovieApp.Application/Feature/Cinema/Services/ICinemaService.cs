using MovieApp.Application.Feature.Cinema.Dtos;

namespace MovieApp.Application.Feature.Cinema.Services;

public interface ICinemaService
{
    Task<List<LocationAndCinema>> GetAllCinemas();
    Task<List<CinemaDetail>> GetCinemaAdmin();
    Task<CinemaStatusResponse> GetAllStatus();
    Task<List<LocationResponse>> GetAllLocation();
    Task<string> SaveCinema(CinemaCreated cinemaRequest);
};