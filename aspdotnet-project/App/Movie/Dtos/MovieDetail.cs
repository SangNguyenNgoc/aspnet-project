using aspdotnet_project.App.Cinema.Dtos;

namespace aspdotnet_project.App.Movie.Dtos;

public class MovieDetail
{
    public string Id { get; set; }

    public string Name { get; set; }
    
    public string SubName { get; set; }

    public string Slug { get; set; }

    public string Description { get; set; }

    public int AgeRestriction { get; set; }

    public string Director { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int RunningTime { get; set; }

    public string HorizontalPoster { get; set; }

    public string Language { get; set; }

    public int SumOfRatings { get; set; }

    public int NumberOfRatings { get; set; }

    public string Performers { get; set; }

    public string Poster { get; set; }

    public string Producer { get; set; }

    public string Trailer { get; set; }

    public MovieStatusDto Status { get; set; } = null!;
    
    public List<CinemaAndShow> Cinemas { get; set; } = null!;
    
    public List<FormatResponse> Formats { get; set; }
    
    public List<GenreResponse> Genres { get; set; }
    
    public class MovieStatusDto
    {
        public long id { get; set; }
        public string description { get; set; }
        public string slug {set;get;}
    }
    
}