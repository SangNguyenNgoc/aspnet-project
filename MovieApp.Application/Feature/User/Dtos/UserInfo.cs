
using MovieApp.Domain.User.Entities;

namespace MovieApp.Application.Feature.User.Dtos;

public class UserInfo
{

    public required string Id { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Avatar { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public required string FullName { get; set; }
    public Gender Gender { get; set; }

}