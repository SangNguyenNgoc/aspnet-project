using System.ComponentModel.DataAnnotations;

namespace aspdotnet_project.App.Auth.Dtos;

public class LoginRequest
{
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }

}