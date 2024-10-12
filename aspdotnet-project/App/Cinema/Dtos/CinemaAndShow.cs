using aspdotnet_project.App.Movie.Dtos;

namespace aspdotnet_project.App.Cinema.Dtos;

public class CinemaAndShow
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public List<FormatAndShow> Formats { get; set; }
    
}