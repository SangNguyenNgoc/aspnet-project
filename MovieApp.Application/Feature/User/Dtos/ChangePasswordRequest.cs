namespace MovieApp.Application.Feature.User.Dtos;

public class ChangePasswordRequest
{
    public string? oldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}