using System.ComponentModel.DataAnnotations;

namespace aspdotnet_project.App.Bill.Dtos;

public class BillCreate
{
    [Required]
    public required string ShowId { get; set; }

    [Required]
    public required List<long> SeatIds { get; set; }
}