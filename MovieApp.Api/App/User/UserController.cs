using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.User.Dtos;
using MovieApp.Application.Feature.User.Service;

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
        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userID)) return BadRequest("User ID not found.");
        var user = await _userService.GetMyProfile(userID);
        if (user == null) return BadRequest("User ID not found.");
        return Ok(user);
    }

    [HttpPut("update")]
    //[Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateRequest userUpdate)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return BadRequest("User ID not found.");
        var result = await _userService.UpdateUser(userId,userUpdate);
        if (!result) return BadRequest("Failed to update user.");
        return Ok(new { message = "Update successfully." });
    }

    [HttpGet("{userId}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await _userService.GetUserById(userId);
        if (user == null) return BadRequest("User ID not found.");
        return Ok(user);
    }


    [HttpPost("change-email")]
    //[Authorize]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return BadRequest("User ID not found.");
        if (string.IsNullOrEmpty(request.newEmail)) return BadRequest("New email is required.");
        var result = await _userService.ChangeEmail(userId, request.newEmail);
        if (!result) return BadRequest("Failed to initiate email change.");
        return Ok("Confirmation email sent.");
    }

    [HttpGet("confirm-email")]
    //[Authorize]
    public async Task<IActionResult> ConfirmEmailChange([FromQuery] string token)
    {
        var result = await _userService.ConfirmEmailChange(token);
        if (!result) return BadRequest("Invalid or expired token.");
        return Ok("Email successfully changed.");
    }

    [HttpPost("send-forgot-password")]
    public async Task<IActionResult> SendForgotPassword([FromBody] SendEmailForgotPasswordRequest request)
    {
        var result = await _userService.SendForgotPassword(request.Email);
        if (!result) return BadRequest("Failed to send change password email.");
        return Ok("Change password email sent.");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ChangePassword([FromQuery] string token, [FromBody] ForgotPasswordRequest request)
    {
        var result = await _userService.ForgotPassword(token, request.newPassword, request.confirmPassword);
        if (!result) return BadRequest("Invalid or expired token.");
        return Ok("Password successfully changed.");
    }

    [HttpPost("update-status")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
    {
        var result = await _userService.UpdateStatus(request.UserId, request.Status);
        if (!result) return BadRequest("Failed to update status.");
        return Ok("Status updated successfully.");
    }

    [HttpPost("change-password")]
    //[Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _userService.ChangePassword(userId, request.oldPassword, request.newPassword, request.confirmPassword);
        if (!result) return BadRequest("Failed to change password.");
        return Ok("Password changed successfully.");
    }
}