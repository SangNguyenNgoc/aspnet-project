namespace MovieApp.Application.Feature.Movie.Dtos;

public class ManageMovie
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string SubName { get; set; }

    public string Slug { get; set; }
    
    public DateOnly ReleaseDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int RunningTime { get; set; }
    
    public int SumOfRatings { get; set; }

    public int NumberOfRatings { get; set; }
    
    public StatusResponse Status { get; set; } = null!;
}