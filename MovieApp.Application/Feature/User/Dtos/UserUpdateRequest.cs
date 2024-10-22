namespace MovieApp.Application.Feature.User.Dtos;

public class UserUpdateRequest{
    public string? PhoneNumber { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? FullName { get; set; }

    public string? Gender { get; set; }

}