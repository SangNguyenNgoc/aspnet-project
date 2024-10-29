using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Cinema.Repositories;

public interface IHallStatusRepository
{
    Task<List<HallStatus>> GetAllHallStatus();
    Task<HallStatus?> GetById(long id);
}