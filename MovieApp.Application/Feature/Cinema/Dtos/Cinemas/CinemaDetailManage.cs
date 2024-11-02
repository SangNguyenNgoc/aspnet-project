namespace MovieApp.Application.Feature.Cinema.Dtos;

public class CinemaDetailManage
{
    public string Id { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string? Hotline { get; set; }
    public string location { get; set; }
    public CinemaStatusResponse Status { get; set; }
    public List<HallDto> Halls { get; set; }
    
    public class HallDto
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public int TotalSeats { get; set; }
        public HallStatusResponse Status { get; set; }
    }
}