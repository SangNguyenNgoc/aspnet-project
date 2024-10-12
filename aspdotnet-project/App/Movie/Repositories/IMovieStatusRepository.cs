using aspdotnet_project.App.Movie.Entities;

namespace aspdotnet_project.App.Movie.Repositories;

public interface IMovieStatusRepository
{
    Task<List<MovieStatus>> FindByIdOrId(long id1, long id2);
}