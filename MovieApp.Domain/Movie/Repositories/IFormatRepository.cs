using MovieApp.Domain.Movie.Entities;

namespace MovieApp.Domain.Movie.Repositories;

public interface IFormatRepository
{
    Task<Entities.Format?> GetById(long id);
    Task<List<Format>> GetAll();
}