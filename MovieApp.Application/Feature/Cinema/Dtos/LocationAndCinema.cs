using MovieApp.Application.Feature.Movie.Dtos;

namespace MovieApp.Application.Feature.Cinema.Dtos;

public class LocationAndCinema
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public List<CinemaDto> Cinemas { get; set; } = null!;
    
    public class CinemaDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<MovieDto> Movies { get; set; } = null!;
        
        public class MovieDto
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
            public List<FormatAndShow> Formats { get; set; }
        }
    }
}