namespace aspdotnet_project.App.Movie.Repositories;

public interface IFormatRepository
{
    Task<Entities.Format?> GetMovieById(long id);
}