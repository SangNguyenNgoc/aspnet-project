using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Cinema.Repositories;

public interface IHallRepository
{
    Task<List<Hall>?> GetHallsByDate(DateOnly date, string cinemaId);
    Task<long> Save(Hall hall);
    Task<Hall?> GetHallById(long hallId);
    Task<Hall> UpdateStatus(Hall hall);
}