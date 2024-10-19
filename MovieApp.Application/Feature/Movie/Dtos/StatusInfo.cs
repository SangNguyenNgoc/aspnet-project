namespace MovieApp.Application.Feature.Movie.Dtos;

public class StatusInfo
{
    public long Id { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public List<MovieInfoLanding> Movies { get; set; } = new();
}