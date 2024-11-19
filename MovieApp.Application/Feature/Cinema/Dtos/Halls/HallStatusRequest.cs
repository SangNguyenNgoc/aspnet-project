using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.Cinema.Dtos;

public class HallStatusRequest
{
    [Required(ErrorMessage = "StatusId is required")]
    public long StatusId { get; set; }
}