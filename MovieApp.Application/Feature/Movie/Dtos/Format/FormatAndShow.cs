using MovieApp.Application.Feature.Show.Dtos;

namespace MovieApp.Application.Feature.Movie.Dtos;

public class FormatAndShow
{
    public long Id { get; set; }
    public string Caption { get; set; }
    public string Version { get; set; }
    public List<ShowResponseInFormat> Shows { get; set; }
}