using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.User.Dtos;

public class ChangePasswordRequest
{
    [Required]
    public required string OldPassword { get; set; }
    
    [Required]
    public required string NewPassword { get; set; }
    
    [Required]
    public required string ConfirmPassword { get; set; }
}