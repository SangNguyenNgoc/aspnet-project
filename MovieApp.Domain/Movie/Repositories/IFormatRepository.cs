namespace MovieApp.Domain.Movie.Repositories;

public interface IFormatRepository
{
    Task<Entities.Format?> GetMovieById(long id);
}