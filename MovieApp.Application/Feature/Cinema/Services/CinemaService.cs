using AutoMapper;
using MovieApp.Application.Exception;
using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Application.Feature.Movie.Services;
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
    private readonly ICinemaStatusRepository _cinemaStatusRepository;

    public CinemaService(IMapper mapper, ICinemaRepository cinemaRepository, ILocationRepository locationRepository, IMovieRepository movieRepository, ICinemaStatusRepository cinemaStatusRepository)
    {
        _mapper = mapper;
        _cinemaRepository = cinemaRepository;
        _locationRepository = locationRepository;
        _movieRepository = movieRepository;
        _cinemaStatusRepository = cinemaStatusRepository;
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

    public async Task<List<CinemaDetail>> GetCinemaAdmin()
    {
        return _mapper.Map<List<CinemaDetail>>(await _cinemaRepository.GetCinemaAdmin());
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
    
    public async Task<string> SaveCinema(CinemaCreated cinemaRequest)
    {
        var location = await _locationRepository.GetLocationById(cinemaRequest.Location) ??
                       throw new DataNotFoundException($"Location with id {cinemaRequest.Location} not found");
        var status = await _cinemaStatusRepository.GetById(cinemaRequest.Status) ??
                     throw new DataNotFoundException($"Status with id {cinemaRequest.Status} not found");
        var cinema = _mapper.Map<Domain.Cinema.Entities.Cinema>(cinemaRequest);
        var slug = AppUtil.GenerateSlug(cinemaRequest.Name);
        cinema.Location = location;
        cinema.Status = status;
        cinema.Slug = slug;
        return await _cinemaRepository.Save(cinema);
    }

    public async Task<CinemaStatusResponse> GetAllStatus()
    {
        return _mapper.Map<CinemaStatusResponse>(await _cinemaStatusRepository.GetAll());
    }

    public async Task<List<LocationResponse>> GetAllLocation()
    {
        return _mapper.Map<List<LocationResponse>>(await _locationRepository.GetAllLocationAndCinema());
    }

}