namespace aspdotnet_project.App.Movie.Dtos;

public class MovieInfoLanding
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SubName { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public int? NumberOfRatings { get; set; }
    public int? SumOfRatings { get; set; }
    public string Poster { get; set; }
    public string Slug { get; set; }
    public int AgeRestriction { get; set; }
    public List<FormatResponse> Formats { get; set; } = new List<FormatResponse>();
    public List<GenreResponse> Genres { get; set; } = new List<GenreResponse>();
    
}