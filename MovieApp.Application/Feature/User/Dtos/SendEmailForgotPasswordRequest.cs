
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.Feature.User.Dtos;

public class SendEmailForgotPasswordRequest
{
    [Required]
    public required string Email { get; set; }
}