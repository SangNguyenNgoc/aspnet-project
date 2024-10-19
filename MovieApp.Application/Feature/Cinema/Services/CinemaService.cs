using AutoMapper;
using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Application.Feature.Show.Dtos;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Domain.Movie.Entities;
using MovieApp.Domain.Movie.Repositories;

namespace MovieApp.Application.Feature.Cinema.Services;

public class CinemaService : ICinemaService
{
    private readonly IMapper _mapper;
    private readonly ICinemaRepository _cinemaRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IMovieRepository _movieRepository;

    public CinemaService(IMapper mapper, ICinemaRepository cinemaRepository, ILocationRepository locationRepository, IMovieRepository movieRepository)
    {
        _mapper = mapper;
        _cinemaRepository = cinemaRepository;
        _locationRepository = locationRepository;
        _movieRepository = movieRepository;
    }

    public async Task<List<LocationAndCinema>> GetAllCinemas()
    {
        var result = new List<LocationAndCinema>();
        var locations = await _locationRepository.GetAllLocationAndCinema();
        var movies = await _movieRepository.GetMovieAndShowByCinemaId();

        foreach (var location in locations.Select(l => _mapper.Map<LocationAndCinema>(l)))
        {
            foreach (var c in location.Cinemas)
            {
                c.Movies = GetMovieAndFormat(c.Id, movies);
            }
            result.Add(location);
        }

        return result;
    }

    private List<LocationAndCinema.CinemaDto.MovieDto> GetMovieAndFormat(string cinemaId, List<Domain.Movie.Entities.Movie> movies)
    {
        var movieResult = movies.Select(m =>
            {
                var movie = _mapper.Map<LocationAndCinema.CinemaDto.MovieDto>(m);
                var format = m.Shows
                    .Where(s => s.Hall.Cinema.Id == cinemaId)
                    .GroupBy(s => s.Format)
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
                movie.Formats = format;
                return movie;
            })
            .Where(m => m.Formats.Count > 0)
            .ToList();
        return movieResult;
    }

}