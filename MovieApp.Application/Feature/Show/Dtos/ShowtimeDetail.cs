using MovieApp.Application.Feature.Movie.Dtos;

namespace MovieApp.Application.Feature.Show.Dtos;

public class ShowtimeDetail
{
    public string Id { get; set; }
    public int RunningTime { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public bool Status { get; set; }
    public FormatResponse Format { get; set; }
    public MovieDto Movie { get; set; }
    public HallDto Hall { get; set; }
    
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
    }
    
    public class HallDto
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public List<SeatDto> Seats { get; set; }
        public CinemaDto Cinema { get; set; }
        
        public class CinemaDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        
        public class SeatDto
        {
            public long Id { get; set; }
            public bool Status { get; set; }
            public int RowIndex { get; set; }
            public string RowName { get; set; }
            public int Order { get; set; }
            public bool isReserved { get; set; }
        }
    }
}