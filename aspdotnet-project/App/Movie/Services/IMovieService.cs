using aspdotnet_project.App.Movie.Dtos;

namespace aspdotnet_project.App.Movie.Services;

public interface IMovieService
{
    Task<List<StatusInfo>> GetMovieToLanding();
    Task<MovieDetail> GetMovieDetail(string slug);
    Task<List<MovieInfoLanding>> GetMovieByStatus(string slug, int page, int pageSize);
}