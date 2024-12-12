using AutoMapper;
using MovieApp.Application.Exception;
using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Application.Feature.Show.Dtos;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Domain.Show.Repositories;

namespace MovieApp.Application.Feature.Show.Services;

public class ShowtimeService : IShowtimeService
{
    private static readonly string[] Messages = ["CinemaId must not be null."];
    private readonly int _cleaningTime = int.Parse(Environment.GetEnvironmentVariable("CLEANING_TIME")!);

    private readonly string _firstShowtime = Environment.GetEnvironmentVariable("FIRST_SHOWTIME")!;
    private readonly IFormatRepository _formatRepository;
    private readonly IHallRepository _hallRepository;
    private readonly string _lastShowtime = Environment.GetEnvironmentVariable("LAST_SHOWTIME")!;
    private readonly IMapper _mapper;
    private readonly IMovieRepository _movieRepository;
    private readonly int _showtimeInterval = int.Parse(Environment.GetEnvironmentVariable("SHOWTIME_INTERVAL")!);
    private readonly IShowtimeRepository _showtimeRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ICinemaRepository _cinemaRepository;

    // ReSharper disable once ConvertToPrimaryConstructor
    public ShowtimeService(IShowtimeRepository showtimeRepository, IMovieRepository movieRepository, IMapper mapper,
        IFormatRepository formatRepository, IHallRepository hallRepository, ITicketRepository ticketRepository, ICinemaRepository cinemaRepository)
    {
        _showtimeRepository = showtimeRepository;
        _movieRepository = movieRepository;
        _mapper = mapper;
        _formatRepository = formatRepository;
        _hallRepository = hallRepository;
        _ticketRepository = ticketRepository;
        _cinemaRepository = cinemaRepository;
    }

    public async Task<List<ShowtimeDetail>> Create(CreateShowtimeRequest createShowtimeRequest)
    {
        var createDate = DateOnly.FromDateTime(createShowtimeRequest.Date);
        await CheckMovieInput(createShowtimeRequest.Movies, createDate);

        if (createShowtimeRequest.CinemaId == null)
            throw new BadRequestException("Invalid", [.. Messages]);
        var halls = await _hallRepository.GetHallsByDate(createDate, createShowtimeRequest.CinemaId) ??
                    throw new BadRequestException("Invalid",["There are halls that have showtime on this date."]);

        var shows = await ScheduleShow(createShowtimeRequest.Movies, halls, createDate);

        var createShows = await _showtimeRepository.Save(shows);
        
        return createShows.Select(s => _mapper.Map<ShowtimeDetail>(s)).ToList();
    }

    public async Task<ShowtimeDetail> GetSeatByShowId(string showId)
    {
        var show = await _showtimeRepository.GetShowByIdCheckDateTime(showId);
        if (show == null) throw new DataNotFoundException($"Show {showId} not found");
        var tickets = await _ticketRepository.GetTicketsByShowId(showId);
        var showtimeDetail = _mapper.Map<ShowtimeDetail>(show);
        showtimeDetail.Hall.Rows = show.Hall.Seats.GroupBy(s => s.RowName).Select(s =>
        {
            var row = new HallResponse.RowDto ();
            row.RowName = s.Key;
            row.Seats = s.Select(seat =>
            {
                var seatDto = _mapper.Map<HallResponse.RowDto.SeatDto>(seat);
                seatDto.isReserved = tickets.Exists(t => t.Seat.Id == seat.Id);
                return seatDto;
            }).ToList();
            return row;
        }).ToList();
        return showtimeDetail;
    }


    private async Task<List<MovieCreateShowtimeRequest>> CheckMovieInput(
        List<MovieCreateShowtimeRequest> movies,
        DateOnly date
    )
    {
        var listMovie = await _movieRepository.GetAllMoviesByDate(date);
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

    private async Task<List<Domain.Show.Entities.Show>> ScheduleShow(List<MovieCreateShowtimeRequest> movies,
        List<Hall> halls,
        DateOnly date
    )
    {
        var currentTime = 0;
        var shows = new List<Domain.Show.Entities.Show>();
        var startDate = new DateTime(date, TimeOnly.Parse(_firstShowtime));
        var endDate = new DateTime(date, TimeOnly.Parse(_lastShowtime));
        var allMovie = await _movieRepository.GetAll();
        var allFormat = await _formatRepository.GetAll();
        while (true)
        {
            var allMovieWithZeroPriority = movies.All(movie => movie.Priority == 0);
            if (allMovieWithZeroPriority)
                foreach (var movie in movies)
                    movie.resetPriority();

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
            if (endDate.CompareTo(showTime) == -1) break;

            var newShow = new Domain.Show.Entities.Show
            {
                Movie = allMovie.Find(m => m.Id == bestMovie.Id)!,
                Format = allFormat.Find(f => f.Id == bestMovie.FormatId)!,
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

    private bool IsRoomAvailable(int currentTime, Hall hall, List<Domain.Show.Entities.Show> shows)
    {
        var show = shows
            .Where(s => s.Hall.Id == hall.Id)
            .MaxBy(s => s.StartTime);
        if (show == null) return true;
        var availableTimeInHall = (show.StartTime.Hour - 9) * 60 + show.StartTime.Minute + show.Movie.RunningTime +
                                  _cleaningTime;

        return availableTimeInHall <= currentTime;
    }

    private Hall? FindAvailableHall(int currentTime, List<Hall> halls, List<Domain.Show.Entities.Show> shows)
    {
        return halls.FirstOrDefault(hall => IsRoomAvailable(currentTime, hall, shows));
    }

    private bool WasRecentlyShow(int currentTime, List<Domain.Show.Entities.Show> shows, MovieCreateShowtimeRequest movie)
    {
        var show = shows
            .Where(s => s.Movie.Id == movie.Id)
            .MaxBy(s => s.StartTime);
        if (show == null) return false;
        var startTimeToIntMinute = (show.StartTime.Hour - 9) * 60 + show.StartTime.Minute;
        return currentTime - startTimeToIntMinute < _showtimeInterval;
    }

    private MovieCreateShowtimeRequest? FindBestMovie(List<Domain.Show.Entities.Show> shows,
        List<MovieCreateShowtimeRequest> movies, int currentTime)
    {
        // Tạo danh sách mới để sắp xếp
        List<MovieCreateShowtimeRequest> sortedMovies = [..movies];

        // Sắp xếp danh sách theo độ ưu tiên giảm dần, nếu bằng nhau thì sắp xếp theo ngày ra mắt mới nhất
        sortedMovies.Sort((a, b) =>
        {
            if (b.Priority != a.Priority) return b.Priority.CompareTo(a.Priority); // Sắp xếp theo độ ưu tiên
            if (b.ReleaseDate > a.ReleaseDate) return 1; // b có ngày ra mắt mới hơn, đặt trước

            if (b.ReleaseDate < a.ReleaseDate) return -1; // a có ngày ra mắt mới hơn, đặt trước
            return 0;
        });

        // Tìm movie không chiếu gần đây
        foreach (var movie in sortedMovies.Where(movie => !WasRecentlyShow(currentTime, shows, movie)))
        {
            movie.decreasePriority();
            return movie;
        }

        return null; // Nếu không có movie nào phù hợp
    }
    
    public async Task<string> Delete(string id)
    {
        var showByIdAfter7day = await _showtimeRepository.GetShowByIdAfter7day(id) ?? 
                                throw new DataNotFoundException($"Showtime with id {id} not found");
        return await _showtimeRepository.Delete(id);
    }
    
    public async Task<ShowtimeDetail> CreateHandWork(CreateShowtimeHandWork createShowtimeHandWork)
    {
        var createDate = DateOnly.FromDateTime(createShowtimeHandWork.Date);
        var startTime = TimeOnly.FromDateTime(createShowtimeHandWork.Date);
        var today = DateOnly.FromDateTime(DateTime.Now);
        if (createDate < today)
            throw new BadRequestException("Invalid", ["Date must be greater than or equal to today."]);
      
        var movie = await _movieRepository.GetMovieById(createShowtimeHandWork.MovieId) ??
                    throw new DataNotFoundException($"Movie with id {createShowtimeHandWork.MovieId} not found");
        var hall = await _hallRepository.GetHallById(createShowtimeHandWork.HallId) ??
                   throw new DataNotFoundException($"Hall with id {createShowtimeHandWork.HallId} not found");
        var cinema = await _cinemaRepository.GetById(createShowtimeHandWork.CinemaId) ??
                     throw new DataNotFoundException($"Cinema with id {createShowtimeHandWork.CinemaId} not found");
        var format = movie.Formats.FirstOrDefault(f => f.Id == createShowtimeHandWork.FormatId) ?? 
                     throw new BadRequestException("Invalid", ["Movie does not have the specified format."]);
        if (hall.Cinema.Id != createShowtimeHandWork.CinemaId)
            throw new BadRequestException("Invalid", ["Hall does not belong to this cinema."]);
        if (!await CheckShowtimeExist(createShowtimeHandWork, movie, hall))
        {
            throw new BadRequestException("Invalid", ["Showtime is conflicted."]);
        }
        var show = new Domain.Show.Entities.Show
        {
            Movie = movie,
            Hall = hall,
            Format = format,
            RunningTime = movie.RunningTime,
            StartDate = createDate,
            StartTime = startTime,
            Status = 1
        };
        var shows = await _showtimeRepository.Save([show]);
        return _mapper.Map<ShowtimeDetail>(show);
    }

    private async Task<bool> CheckShowtimeExist(CreateShowtimeHandWork createShowtimeHandWork, Domain.Movie.Entities.Movie movie, Hall hall)
    {
        var showExist = await _showtimeRepository.CheckShowtimeByTime(createShowtimeHandWork.Date, hall.Id);
        return showExist.All(s => s.StartTime > TimeOnly.FromDateTime(createShowtimeHandWork.Date).AddMinutes(movie.RunningTime) ||
                                  s.StartTime.AddMinutes(s.Movie.RunningTime) < TimeOnly.FromDateTime(createShowtimeHandWork.Date));
    }
    
}