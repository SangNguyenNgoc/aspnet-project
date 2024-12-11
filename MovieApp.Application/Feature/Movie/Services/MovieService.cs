using AutoMapper;
using Microsoft.SqlServer.Server;
using MovieApp.Application.Exception;
using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Application.Feature.Show.Dtos;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Domain.Movie.Entities;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Infrastructure.S3;
using Format = MovieApp.Domain.Movie.Entities.Format;

namespace MovieApp.Application.Feature.Movie.Services;

public class MovieService : IMovieService
{
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IMapper _mapper;
    private readonly IMovieRepository _movieRepository;
    private readonly IMovieStatusRepository _movieStatusRepository;
    private readonly IFormatRepository _formatRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly S3Service _s3Service;

    public MovieService(IMapper mapper, IMovieRepository movieRepository, IMovieStatusRepository movieStatusRepository,
        ICinemaRepository cinemaRepository, S3Service s3Service, IFormatRepository formatRepository, IGenreRepository genreRepository)
    {
        _mapper = mapper;
        _movieRepository = movieRepository;
        _movieStatusRepository = movieStatusRepository;
        _cinemaRepository = cinemaRepository;
        _s3Service = s3Service;
        _formatRepository = formatRepository;
        _genreRepository = genreRepository;
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

    public async Task<string> CreateMovie(MovieCreateRequest movieCreateRequest)
    {
        var newMovie = _mapper.Map<Domain.Movie.Entities.Movie>(movieCreateRequest);
        var slug = AppUtil.GenerateSlug(newMovie.Name);
        var status = await _movieStatusRepository.FindById(movieCreateRequest.Status)
            ?? throw new DataNotFoundException($"Status {movieCreateRequest.Status} not found");
        
        newMovie.HorizontalPoster = await _s3Service.UploadFile(movieCreateRequest.HorizontalPoster, slug + "-horizontal", "posters");
        newMovie.Poster = await _s3Service.UploadFile(movieCreateRequest.Poster, slug, "posters");
        newMovie.Slug = slug;
        
        newMovie.Status = status;
        
        newMovie.Formats = await GetFormats(movieCreateRequest.Formats);
        newMovie.Genres = await GetGenres(movieCreateRequest.Genres);
        newMovie.Description = AppUtil.SanitizeHtml(movieCreateRequest.Description);
        
        return await _movieRepository.Save(newMovie);
    }
    
    public async Task<List<StatusResponse>> GetAllStatus()
    {
        var status = await _movieStatusRepository.GetAll();
        return _mapper.Map<List<StatusResponse>>(status);
    }
    
    public async Task<List<ManageMovie>> GetAllMovies()
    {
        var movies = await _movieRepository.GetAll();
        return _mapper.Map<List<ManageMovie>>(movies);
    }
    
    public async Task<MovieDetail> GetMovieById(string id)
    {
        var movie = await _movieRepository.GetMovieById(id) ??
                    throw new DataNotFoundException($"Movie {id} not found");
        return _mapper.Map<MovieDetail>(movie);
    }

    public async Task<string> UpdateMovie(string id, MovieUpdateRequest movieCreateRequest)
    {
        var movie = await _movieRepository.GetMovieById(id) ??
                    throw new DataNotFoundException($"Movie {id} not found");
        var slug = AppUtil.GenerateSlug(movieCreateRequest.Name);
        var status = await _movieStatusRepository.FindById(movieCreateRequest.Status)
                     ?? throw new DataNotFoundException($"Status {movieCreateRequest.Status} not found");

        if (movieCreateRequest.HorizontalPoster != null)
            movie.HorizontalPoster =
                await _s3Service.UploadFile(movieCreateRequest.HorizontalPoster, slug + "-horizontal", "posters");
        if (movieCreateRequest.Poster != null)
            movie.Poster = await _s3Service.UploadFile(movieCreateRequest.Poster, slug, "posters");
        movie.Slug = slug;
        
        movie.Status = status;
        
        movie.Formats = await GetFormats(movieCreateRequest.Formats);
        movie.Genres = await GetGenres(movieCreateRequest.Genres);
        movie.Description = AppUtil.SanitizeHtml(movieCreateRequest.Description);
        
        movie.Name = movieCreateRequest.Name;
        movie.SubName = movieCreateRequest.SubName;
        movie.AgeRestriction = movieCreateRequest.AgeRestriction;
        movie.Director = movieCreateRequest.Director;
        movie.ReleaseDate = movieCreateRequest.ReleaseDate;
        movie.EndDate = movieCreateRequest.EndDate;
        movie.RunningTime = movieCreateRequest.RunningTime;
        movie.Producer = movieCreateRequest.Producer;
        movie.Trailer = movieCreateRequest.Trailer;
        movie.Language = movieCreateRequest.Language;
        movie.Performers = movieCreateRequest.Performers;
        
        return await _movieRepository.Update(movie);
    }

    private async Task<List<Format>> GetFormats(List<long> formatIds)
    {
        var formats = await _formatRepository.GetAll();
        if (formatIds.Any(id => !formats.Exists(f => f.Id == id)))
        {
            throw new DataNotFoundException($"Format not found");
        }
        return formats.Where(f => formatIds.Contains(f.Id)).ToList();
    }

    private async Task<List<Genre>> GetGenres(List<long> genreIds)
    {
        var genres = await _genreRepository.GetAll();
        if (genreIds.Any(id => !genres.Exists(g => g.Id == id)))
        {
            throw new DataNotFoundException($"Genre not found");
        }

        return genres.Where(g => genreIds.Contains(g.Id)).ToList();
    }
}