using System.ComponentModel.DataAnnotations;

namespace aspdotnet_project.App.Auth.Dtos;

public class RegisterRequest
{
    
    [Required]
    public required string FullName { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }

}