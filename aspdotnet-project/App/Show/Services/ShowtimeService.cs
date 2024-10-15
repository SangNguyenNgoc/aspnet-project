using aspdotnet_project.App.Cinema.Entities;
using aspdotnet_project.App.Cinema.Repositories;
using aspdotnet_project.App.Movie.Dtos;
using aspdotnet_project.App.Movie.Repositories;
using aspdotnet_project.App.Show.Dtos;
using aspdotnet_project.App.Show.Repositories;
using aspdotnet_project.Exception;
using AutoMapper;

namespace aspdotnet_project.App.Show.Services;

public class ShowtimeService : IShowtimeService
{
    private readonly IMapper _mapper;
    private readonly IMovieRepository _movieRepository;
    private readonly IShowtimeRepository _showtimeRepository;
    private readonly IFormatRepository _formatRepository;
    private readonly IHallRepository _hallRepository;
    private readonly ITicketRepository _ticketRepository;
    
    private readonly string _firstShowtime = Environment.GetEnvironmentVariable("FIRST_SHOWTIME")!;
    private readonly string _lastShowtime = Environment.GetEnvironmentVariable("LAST_SHOWTIME")!;
    private readonly int _showtimeInterval = int.Parse(Environment.GetEnvironmentVariable("SHOW_INTERVAL")!);
    private readonly int _cleaningTime = int.Parse(Environment.GetEnvironmentVariable("CLEANING_TIME")!);

    // ReSharper disable once ConvertToPrimaryConstructor
    public ShowtimeService(IShowtimeRepository showtimeRepository, IMovieRepository movieRepository, IMapper mapper, IFormatRepository formatRepository, IHallRepository hallRepository, ITicketRepository ticketRepository)
    {
        _showtimeRepository = showtimeRepository;
        _movieRepository = movieRepository;
        _mapper = mapper;
        _formatRepository = formatRepository;
        _hallRepository = hallRepository;
        _ticketRepository = ticketRepository;
    }


     private async Task<List<MovieCreateShowtimeRequest>> CheckMovieInput(
        List<MovieCreateShowtimeRequest> movies, 
        DateOnly date
        )
    {
        var listMovie = await _movieRepository.GetAllMovies(date);
        foreach (var movie in movies)
        {
            // Tìm phim trong danh sách
            var foundMovie = listMovie.Find(m => m.Id == movie.Id);

            // Nếu không tìm thấy phim, ném ra ngoại lệ
            if (foundMovie == null) throw new DataNotFoundException($"Movie {movie.Id} not found");

            var hasFormat = foundMovie.Formats
                .ToList()
                .Find(f => f.Id == movie.FormatId) != null;

            if (!hasFormat)
                throw new DataNotFoundException(
                    $"Movie with ID {movie.Id} does not have the specified format ID {movie.FormatId}.");

            movie.Duration = foundMovie.RunningTime;
            movie.ReleaseDate = foundMovie.ReleaseDate;
            movie.OriginalPriority = movie.Priority;
        }

        // Trả về danh sách các bộ phim đã kiểm tra và có thể đã xử lý logic
        return movies;
    }

    public async Task<List<Entities.Show>> Create(CreateShowtimeRequest createShowtimeRequest)
    {
        DateOnly createDate = DateOnly.FromDateTime(createShowtimeRequest.Date);
        await CheckMovieInput(createShowtimeRequest.Movies, createDate);

        if (createShowtimeRequest.CinemaId != null)
        {
            List<Hall>? halls = await _hallRepository.GetHallsByDate(createDate, createShowtimeRequest.CinemaId);
        
            List<Entities.Show> shows = await ScheduleShow(createShowtimeRequest.Movies, halls, createDate);

            return await _showtimeRepository.Save(shows);
        }
        throw new BadRequestException("Invalid", new List<string>(new string[] { "CinemaId must not be null." }));
    }
    
    public async Task<ShowtimeDetail> GetSeatByShowId(string showId)
    {
        var show = await _showtimeRepository.GetShowByIdCheckDateTime(showId);
        if (show == null) throw new DataNotFoundException($"Show {showId} not found");
        var tickets = await _ticketRepository.getTicketsByShowId(showId);
        var showtimeDetail = _mapper.Map<ShowtimeDetail>(show);
        foreach (var t in tickets)
        {
            showtimeDetail.Hall.Seats.Find(s => s.Order == t.Seat.Order)!.isReserved = true;
        }
        return showtimeDetail;
    }

    private async Task<List<Entities.Show>> ScheduleShow(List<MovieCreateShowtimeRequest> movies, 
        List<Hall> halls, 
        DateOnly date
        )
    {
        var currentTime = 0;
        var shows = new List<Entities.Show>();
        DateTime startDate = new DateTime(date, TimeOnly.Parse(_firstShowtime));
        DateTime endDate = new DateTime(date, TimeOnly.Parse(_lastShowtime));
        while (true)
        {
            bool allMovieWithZeroPriority = movies.All(movie => movie.Priority == 0);
            if (allMovieWithZeroPriority)
            {
                foreach (var movie in movies)
                {
                    movie.resetPriority();
                }
            }

            var hall = FindAvailableHall(currentTime, halls, shows);
            if (hall == null)
            {
                currentTime += 15;
                // Console.WriteLine(currentTime);
                continue;
            }
            
            var bestMovie = FindBestMovie(shows, movies, currentTime);
            if (bestMovie == null)
            {
                currentTime += 15;
                continue;
            }

            var showTime = startDate.AddMinutes(currentTime);
            if (endDate.CompareTo(showTime) == -1)
            {
                break;
            }

            var newShow = new Entities.Show
            {
                Movie = await _movieRepository.GetMovieById(bestMovie.Id!),
                Format = await _formatRepository.GetMovieById(bestMovie.FormatId),
                Hall = hall,
                RunningTime = bestMovie.Duration,
                StartDate = DateOnly.FromDateTime(startDate),
                StartTime = TimeOnly.FromDateTime(startDate).AddMinutes(currentTime),
                Status = 1
            };
            shows.Add(newShow);
        }
        return shows;
    }
    
    private bool IsRoomAvailable(int currentTime, Hall hall, List<Entities.Show> shows) {
        var show = shows
            .Where(s => s.Hall.Id == hall.Id)  // Lọc các buổi chiếu thuộc hội trường cụ thể
            .OrderByDescending(s => s.StartTime)  // Sắp xếp theo StartTime giảm dần
            .FirstOrDefault();
        if( show == null) return true;
        var availableTimeInHall = (show.StartTime.Hour - 9) * 60 + show.StartTime.Minute + show.Movie.RunningTime + _cleaningTime;
       
        return availableTimeInHall <= currentTime;
    }

    private Hall? FindAvailableHall(int currentTime, List<Hall> halls, List<Entities.Show> shows)
    {
        foreach (var hall in halls)
        {
            if (IsRoomAvailable(currentTime, hall, shows))
            {
                return hall;
            }
        }
        return null;
    }

    private bool WasRecentlyShow(int currentTime, List<Entities.Show> shows, MovieCreateShowtimeRequest movie)
    {
        var show = shows
            .Where(s => s.Movie.Id == movie.Id)  // Lọc các buổi chiếu thuộc hội trường cụ thể
            .OrderByDescending(s => s.StartTime)  // Sắp xếp theo StartTime giảm dần
            .FirstOrDefault();
        if( show == null) return false;
        var startTimeToIntMinute = (show.StartTime.Hour - 9) * 60 + show.StartTime.Minute;
        return currentTime - startTimeToIntMinute < _showtimeInterval;
    }
    
    private MovieCreateShowtimeRequest? FindBestMovie(List<Entities.Show> shows, List<MovieCreateShowtimeRequest> movies, int currentTime)
    {
        // Tạo danh sách mới để sắp xếp
        List<MovieCreateShowtimeRequest> sortedMovies = new List<MovieCreateShowtimeRequest>(movies);

        // Sắp xếp danh sách theo độ ưu tiên giảm dần, nếu bằng nhau thì sắp xếp theo ngày ra mắt mới nhất
        sortedMovies.Sort((a, b) =>
        {
            if (b.Priority == a.Priority)
            {
                if (b.ReleaseDate > a.ReleaseDate)
                {
                    return 1; // b có ngày ra mắt mới hơn, đặt trước
                }
                else if (b.ReleaseDate < a.ReleaseDate)
                {
                    return -1; // a có ngày ra mắt mới hơn, đặt trước
                }
                return 0;; // Ngày ra mắt mới nhất được ưu tiên
            }
            return b.Priority.CompareTo(a.Priority); // Sắp xếp theo độ ưu tiên
        });

        // Tìm movie không chiếu gần đây
        foreach (var movie in sortedMovies)
        {
            if (!WasRecentlyShow(currentTime, shows, movie))
            {
                movie.decreasePriority();
                return movie;
            }
        }
        return null; // Nếu không có movie nào phù hợp
    }

}