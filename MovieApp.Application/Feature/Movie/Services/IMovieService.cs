using MovieApp.Application.Feature.Movie.Dtos;

namespace MovieApp.Application.Feature.Movie.Services;

public interface IMovieService
{
    Task<List<StatusInfo>> GetMovieToLanding();
    Task<MovieDetail> GetMovieDetail(string slug);
    Task<List<MovieInfoLanding>> GetMovieByStatus(string slug, int page, int pageSize);
    Task<string> CreateMovie(MovieCreateRequest movieCreateRequest);
    Task<List<StatusResponse>> GetAllStatus();
    Task<List<ManageMovie>> GetAllMovies();
    Task<MovieDetail> GetMovieById(string id);
}