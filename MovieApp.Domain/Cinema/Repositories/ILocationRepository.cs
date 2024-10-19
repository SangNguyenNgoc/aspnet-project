using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Cinema.Repositories;

public interface ILocationRepository
{
    Task<List<Location>> GetAllLocationAndCinema();
}