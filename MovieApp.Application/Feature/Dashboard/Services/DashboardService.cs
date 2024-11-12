using MovieApp.Application.Exception;
using MovieApp.Application.Feature.Dashboard.Dtos;
using MovieApp.Domain.Bill.Repositories;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Domain.Show.Repositories;

namespace MovieApp.Application.Feature.Dashboard.Services;

public class DashboardService : IDashboardService
{
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IBillRepository _billRepository;  
    private readonly ITicketRepository _ticketRepository;
    private readonly IShowtimeRepository _showtimeRepository;
    
    public DashboardService(ICinemaRepository cinemaRepository, IMovieRepository movieRepository, ITicketRepository ticketRepository, IShowtimeRepository showtimeRepository, IBillRepository billRepository)
    {
        _cinemaRepository = cinemaRepository;
        _movieRepository = movieRepository;
        _ticketRepository = ticketRepository;
        _showtimeRepository = showtimeRepository;
        _billRepository = billRepository;
    }
    
    public async Task<List<MovieDashboard>?> StatisticMovie(int month, int year)
    {
        if (year is < 1 or > 9999)
        {
            throw new BadRequestException("Year is invalid");
        }

        if (month is < 1 or > 12)
        {
            throw new BadRequestException("Month is invalid");
        }
        
        var movies = await _movieRepository.GetMovieByYear(year);
        
        return movies?.Where(m => m.Shows.Any(s => s.StartDate.Month == month))?
            .Select(movie => new MovieDashboard
            {
                id = movie.Id, 
                name = movie.Name, 
                numberOfTickets = movie.Shows.Where(s => s.StartDate.Month == month)
                    .Sum(s => s.Tickets.Count), 
                revenues = movie.Shows.Where(s => s.StartDate.Month == month)
                    .Sum(s => s.Tickets.Sum(t => t.Seat.Type.Price))
            })
            .OrderByDescending(m => m.revenues)
            .ToList();
    }
    
    public async Task<List<CinemaDashboard>?> StatisticCinema(int month, int year)
    {
        if (year is < 1 or > 9999)
        {
            throw new BadRequestException("Year is invalid");
        }

        if (month is < 1 or > 12)
        {
            throw new BadRequestException("Month is invalid");
        }
        var cinemas = await _cinemaRepository.GetCinemaByYear(year);
        
        return cinemas?.Where(c => c.Halls.Any(h => h.Shows.Any(s => s.StartDate.Month == month)))?
            .Select(cinema => new CinemaDashboard
            {
                id = cinema.Id, 
                name = cinema.Name, 
                numberOfTickets = cinema.Halls.Sum(h => h.Shows.Where(s => s.StartDate.Month == month)
                    .Sum(s => s.Tickets.Count)), 
                revenues = cinema.Halls.Sum(h => h.Shows.Where(s => s.StartDate.Month == month)
                    .Sum(s => s.Tickets.Sum(t => t.Seat.Type.Price)))
            })
            .OrderByDescending(c => c.revenues)
            .ToList();
    }
    
    
    public async Task<List<Chart>> GetDashboard(int year)
    {
        if (year is < 1 or > 9999)
        {
            throw new BadRequestException("Year is invalid");
        }
        var bill = await _billRepository.GetBillByYear(year) ?? 
                   throw new DataNotFoundException("Bill not found");
        var chart = new List<Chart>();
        for (var i = 1; i <= 12; i++)
        {
            var numberOfTickets = 0;
            var revenues = 0L;
            var numberOfBill = 0;
            if (bill.Where(b => b.CreateAt.Month == i) != null)
            {
                numberOfTickets = bill.Where(b => b.CreateAt.Month == i).Sum(b => b.Tickets.Count);
                revenues = bill.Where(b => b.CreateAt.Month == i).Sum(b => b.Total);
                numberOfBill = bill.Count(b => b.CreateAt.Month == i);
            }
            chart.Add(new Chart
            {
                month = i,
                numberOfTickets = numberOfTickets,
                revenues = revenues,
                numberOfBill = numberOfBill,
            });
        }

        return chart;
    }
}