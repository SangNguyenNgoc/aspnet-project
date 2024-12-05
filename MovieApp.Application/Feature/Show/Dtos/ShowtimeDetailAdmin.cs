using MovieApp.Application.Feature.Movie.Dtos;

namespace MovieApp.Application.Feature.Show.Dtos;

public class ShowtimeDetailAdmin
{
    public string Id { get; set; }
    public int RunningTime { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public bool Status { get; set; }
    public FormatResponse Format { get; set; }
    public ShowtimeDetail.MovieDto Movie { get; set; }
    
}