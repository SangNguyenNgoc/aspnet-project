using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Cinema.Repositories;

public interface ICinemaStatusRepository
{
    Task<List<CinemaStatus>> GetAll();
    Task<CinemaStatus?> GetById(long id);
}