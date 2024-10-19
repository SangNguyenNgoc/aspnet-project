namespace MovieApp.Application.Feature.User.Dtos;

public class ForgotPasswordRequest
{
    public string? newPassword { get; set; }
    public string? confirmPassword { get; set; }
}