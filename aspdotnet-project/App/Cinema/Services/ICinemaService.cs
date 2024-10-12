using aspdotnet_project.App.Cinema.Dtos;

namespace aspdotnet_project.App.Cinema.Services;

public interface ICinemaService
{
    Task<List<LocationAndCinema>> GetAllCinemas();
};