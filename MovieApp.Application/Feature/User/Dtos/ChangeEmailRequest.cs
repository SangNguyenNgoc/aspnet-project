using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.User.Dtos;

public class ChangeEmailRequest
{
    [Required]
    public required string NewEmail { get; set; }
}