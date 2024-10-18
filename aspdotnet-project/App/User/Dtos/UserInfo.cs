
namespace aspdotnet_project.App.User.Dtos;

using aspdotnet_project.App.User.Entities;
using Microsoft.AspNetCore.Identity;

public class UserInfo{

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Avatar { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? FullName { get; set; }

    public string? Gender { get; set; }

}