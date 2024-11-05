using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Exception;
using MovieApp.Application.Feature.User.Dtos;
using MovieApp.Application.Feature.User.Service;
using UserUpdateRequest = MovieApp.Application.Feature.User.Dtos.UserUpdateRequest;

namespace MovieApp.Api.App.User;

[Route("/api/v1/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("all")]
    //[Authorize(Roles = "Admin")]
    public async Task<List<UserInfo>> GetAllUsers()
    {
        return await _userService.GetAllUsers();
    }

    [HttpGet("profile")]
    //[Authorize]
    public async Task<IActionResult> MyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new DataNotFoundException("Token empty or invalid.");
        var user = await _userService.GetMyProfile(userId);
        return Ok(user);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateRequest userUpdate)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new DataNotFoundException("Token empty or invalid.");
        var result = await _userService.UpdateUser(userId, userUpdate);
        return Ok(result);
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await _userService.GetUserById(userId);
        return Ok(user);
    }


    [HttpPost("change-email")]
    [Authorize]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new DataNotFoundException("Token empty or invalid.");
        var result = await _userService.ChangeEmail(userId, request.NewEmail);
        return Ok("Confirmation email sent.");
    }

    [HttpGet("confirm-email")]
    [Authorize]
    public async Task<IActionResult> ConfirmEmailChange([FromQuery] string token)
    {
        await _userService.ConfirmEmailChange(token);
        return Ok("Email successfully changed.");
    }

    [HttpPost("send-forgot-password")]
    public async Task<IActionResult> SendForgotPassword([FromBody] SendEmailForgotPasswordRequest request)
    {
        await _userService.SendForgotPassword(request.Email);
        return Ok("Change password email sent.");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ChangePassword([FromQuery] string token, [FromBody] ForgotPasswordRequest request)
    {
        var result = await _userService.ForgotPassword(token, request.NewPassword, request.ConfirmPassword);
        return Ok("Password successfully changed.");
    }

    [HttpPost("update-status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
    {
        await _userService.UpdateStatus(request.UserId, request.Status);
        return Ok("Status updated successfully.");
    }

    [HttpPost("change-password")]
    //[Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                     throw new BadRequestException("Token empty or invalid");
        var result = await _userService.ChangePassword(userId, request);
        return Ok("Password changed successfully.");
    }
}