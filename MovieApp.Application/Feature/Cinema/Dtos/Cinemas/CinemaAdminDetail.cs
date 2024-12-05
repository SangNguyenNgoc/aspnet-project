using MovieApp.Application.Feature.Show.Dtos;

namespace MovieApp.Application.Feature.Cinema.Dtos;

public class CinemaAdminDetail
{
    public string Id { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string? Hotline { get; set; }
    public string Location { get; set; }
    public CinemaStatusResponse Status { get; set; }
    public List<HallAdmin> Halls { get; set; }
    
    public class HallAdmin
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public int TotalSeats { get; set; }
        public HallStatusResponse Status { get; set; }
        public List<ShowtimeDetailAdmin> Showtimes { get; set; }
    }
}