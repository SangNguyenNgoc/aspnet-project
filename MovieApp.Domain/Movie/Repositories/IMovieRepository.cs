namespace MovieApp.Domain.Movie.Repositories;

public interface IMovieRepository
{
    Task<Entities.Movie> GetMovieById(string id);
    Task<List<Entities.Movie>> GetAllMovies(DateOnly date);
    Task<Entities.Movie?> GetMovieBySlug(string slug);
    Task<List<Entities.Movie>> GetMovieByStatusAndOrderByRating(string slug);
    Task<List<Entities.Movie>> GetMovieAndShowByCinemaId();
    Task<string> Save(Domain.Movie.Entities.Movie movie);
}