namespace MovieApp.Application.Feature.Cinema.Dtos;

public class HallResponse
{
    public long Id { get; set; }
    public String Name { get; set; }
    public int TotalSeats { get; set; }
    public List<RowDto> Rows { get; set; }
    public CinemaDto Cinema { get; set; }
        
    public class CinemaDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
        
    public class RowDto
    {
        public string RowName { get; set; }
        public List<SeatDto> Seats { get; set; }
            
        public class SeatDto
        {
            public long Id { get; set; }
            public bool Status { get; set; }
            public int RowIndex { get; set; }
            // public string RowName { get; set; }
            public int Order { get; set; }
            public bool isReserved { get; set; }
            public SeatTypeResponse SeatType { get; set; }
        }
    }
}