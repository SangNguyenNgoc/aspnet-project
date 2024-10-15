using aspdotnet_project.App.Cinema.Entities;

namespace aspdotnet_project.App.Cinema.Repositories;

public interface ILocationRepository
{
    Task<List<Location>> GetAllLocationAndCinema();
}