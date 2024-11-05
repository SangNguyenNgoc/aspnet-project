namespace MovieApp.Application.Feature.Bill.Dtos;

public class BillDetail
{
    public required string Id { get; set; }
    public required DateTime CreateAt { get; set; }
    public required DateTime ExpireAt { get; set; }
    public string? FailureReason { get; set; } = string.Empty;
    public DateTime? PaymentAt { get; set; }
    public DateTime? FailureAt { get; set; }
    public required string PaymentUrl { get; set; }
    public required long Total { get; set; }
    public required BillStatusDto Status { get; set; }
    public required ShowDtoInBillDetail Show { get; set; }
    public required MovieDtoInBillDetail Movie { get; set; }
    public required ICollection<TicketDtoInBillDetail> Tickets { get; set; } 
    public required UserDtoInBillDetail Customer { get; set; }
    public required CinemaDtoInBillDetail Cinema { get; set; }
    
    public class ShowDtoInBillDetail
    {
        public required string Id { get; set; }
        public required int RunningTime { get; set; }
        public required DateOnly StartDate { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required string Format { get; set; }
    }

    public class MovieDtoInBillDetail
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string SubName { get; set; } 
        public required string Poster { get; set; } 
        public required int AgeRestriction { get; set; }
        public required string HorizontalPoster { get; set; } 
    }

    public class TicketDtoInBillDetail
    {
        public required string Id { get; set; }
        public required string SeatName { get; set; }
        public required string Type { get; set; }
        public required long Price { get; set; }
    }
    
    public class UserDtoInBillDetail
    {
        public required string? Fullname { get; set; }
        public required string Email { get; set; }
    }

    public class CinemaDtoInBillDetail
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string HallName { get; set; }
    }
    
}