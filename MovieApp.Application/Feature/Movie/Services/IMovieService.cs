using MovieApp.Application.Feature.Movie.Dtos;

namespace MovieApp.Application.Feature.Movie.Services;

public interface IMovieService
{
    Task<List<StatusInfo>> GetMovieToLanding();
    Task<MovieDetail> GetMovieDetail(string slug);
    Task<List<MovieInfoLanding>> GetMovieByStatus(string slug, int page, int pageSize);
}