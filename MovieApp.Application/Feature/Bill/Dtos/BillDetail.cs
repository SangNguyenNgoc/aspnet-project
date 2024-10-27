namespace MovieApp.Application.Feature.Bill.Dtos;

public class BillDetail
{
    public string Id { get; set; } = DateTime.Now.Ticks.ToString();

    public DateTime CreateAt { get; set; }
    public DateTime ExpireAt { get; set; }
    public string? FailureReason { get; set; } = string.Empty;
    public DateTime? PaymentAt { get; set; }
    public DateTime? FailureAt { get; set; }
    public required string PaymentUrl { get; set; }
    public long Total { get; set; }
    public required BillStatusDto Status { get; set; }
    public required ShowDtoInBillDetail Show { get; set; }
    public required MovieDtoInBillDetail Movie { get; set; }
    public required ICollection<TicketDtoInBillDetail> Tickets { get; set; } 
    public required UserDtoInBillDetail Customer { get; set; }
    
    public class ShowDtoInBillDetail
    {
        public required string Id { get; set; }
        public int RunningTime { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public string Format { get; set; }
    }

    public class MovieDtoInBillDetail
    {
        public required string Id { get; set; }
        public string Name { get; set; } = null!;
        public string SubName { get; set; } = null!;
        public string Poster { get; set; } = null!;
        public string HorizontalPoster { get; set; } = null!;
    }

    public class TicketDtoInBillDetail
    {
        public required string Id { get; set; }
        public required string SeatName { get; set; }
    }
    
    public class UserDtoInBillDetail
    {
        public required string Fullname;
        public required string Email;
    }
    
}

