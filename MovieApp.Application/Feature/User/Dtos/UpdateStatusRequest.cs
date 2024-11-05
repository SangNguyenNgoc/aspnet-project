using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.User.Dtos;

public class UpdateStatusRequest
{
    [Required]
    public required string UserId { get; set; }
    
    [Required]
    public required int Status { get; set; }
}