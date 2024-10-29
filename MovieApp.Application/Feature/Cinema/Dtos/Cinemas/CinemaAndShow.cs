using MovieApp.Application.Feature.Movie.Dtos;

namespace MovieApp.Application.Feature.Cinema.Dtos;

public class CinemaAndShow
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public List<FormatAndShow> Formats { get; set; }
    
}