using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Movie.Dtos;

namespace MovieApp.Application.Feature.Dashboard.Dtos;

public class StatisticsOfTime
{
    public int numberOfTickets { get; set; }
    public long revenues { get; set; }
    public int numberOfBill { get; set; }
    public ManageMovie? bestMovie { get; set; } = null!;
    public CinemaDetail? bestCinema { get; set; } = null!;
}