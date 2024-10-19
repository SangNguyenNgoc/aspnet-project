using AutoMapper;
using MovieApp.Application.Exception;
using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Application.Feature.Show.Dtos;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Domain.Movie.Repositories;

namespace MovieApp.Application.Feature.Movie.Services;

public class MovieService : IMovieService
{
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IMapper _mapper;
    private readonly IMovieRepository _movieRepository;
    private readonly IMovieStatusRepository _movieStatusRepository;

    public MovieService(IMapper mapper, IMovieRepository movieRepository, IMovieStatusRepository movieStatusRepository,
        ICinemaRepository cinemaRepository)
    {
        _mapper = mapper;
        _movieRepository = movieRepository;
        _movieStatusRepository = movieStatusRepository;
        _cinemaRepository = cinemaRepository;
    }

    public async Task<List<StatusInfo>> GetMovieToLanding()
    {
        var movieStatusList = await _movieStatusRepository.FindByIdOrId(1, 2);

        return movieStatusList
            .Select(movieStatus =>
            {
                var statusAndMovie = _mapper.Map<StatusInfo>(movieStatus);
                var movieInfoLandings = statusAndMovie.Movies;

                if (movieInfoLandings.Count == 0) return statusAndMovie;
                var moviesAfterSort = movieInfoLandings
                    .OrderBy(m => m.ReleaseDate)
                    .Take(5)
                    .ToList();

                statusAndMovie.Movies = moviesAfterSort;

                return statusAndMovie;
            })
            .ToList();
    }

    public async Task<MovieDetail> GetMovieDetail(string slug)
    {
        var movie = await _movieRepository.GetMovieBySlug(slug);
        if (movie != null) return await AddShowToMovie(movie);
        throw new DataNotFoundException($"Movie {slug} not found");
    }

    public async Task<List<MovieInfoLanding>> GetMovieByStatus(string slug, int page, int pageSize)
    {
        var movies = await _movieRepository.GetMovieByStatusAndOrderByRating(slug);

        return movies
            .Select(m => _mapper.Map<MovieInfoLanding>(m))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    private async Task<MovieDetail> AddShowToMovie(Domain.Movie.Entities.Movie movie)
    {
        var movieDetail = _mapper.Map<MovieDetail>(movie);
        var toDate = DateOnly.FromDateTime(DateTime.Today);
        var cinemas = await _cinemaRepository.GetCinemasByStatusOrderByCreateDate(
            movie.Slug,
            toDate,
            toDate.AddDays(7)
        );
        var cinemaAndShow = cinemas.Select(cinema =>
        {
            var result = _mapper.Map<CinemaAndShow>(cinema);
            var formats = cinema.Halls
                .SelectMany(h => h.Shows)
                .GroupBy(show => show.Format)
                .Select(group =>
                {
                    var format = _mapper.Map<FormatAndShow>(group.Key);
                    var show = group
                        .Select(s => _mapper.Map<ShowResponseInFormat>(s))
                        .OrderBy(s => s.StartDate)
                        .ThenBy(s => s.StartTime)
                        .ToList();
                    format.Shows = show;
                    return format;
                })
                .ToList();

            result.Formats = formats;
            return result;
        }).ToList();

        movieDetail.Cinemas = cinemaAndShow;
        return movieDetail;
    }
}