using System.ComponentModel.DataAnnotations;

namespace MovieApp.Identity.Dtos;

public class LoginRequest
{
    [Required] public string? Email { get; set; }

    [Required] public string? Password { get; set; }
}