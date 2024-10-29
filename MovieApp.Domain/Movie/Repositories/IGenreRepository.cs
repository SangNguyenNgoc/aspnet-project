using MovieApp.Domain.Movie.Entities;

namespace MovieApp.Domain.Movie.Repositories;

public interface IGenreRepository
{
    Task<List<Genre>> GetAll();
    Task<Entities.Genre?> GetById(long id);
}