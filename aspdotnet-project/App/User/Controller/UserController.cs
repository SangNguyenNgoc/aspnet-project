using System.Security.Claims;
using aspdotnet_project.App.User.Dtos;
using aspdotnet_project.App.User.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnet_project.App.User.Controller;

[Route("/api/v1/user")]
[ApiController]
public class UserController: ControllerBase{
    public readonly UserService _userService;

    public UserController(UserService userService){
        _userService = userService;
    }

    [HttpGet("/all")]
    public async Task<List<UserInfo>> GetAllUsers(){
        return await _userService.GetAllUsers();
    }

    [HttpGet("/profile")]
    [Authorize]
    public async Task<IActionResult> MyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)){
            return BadRequest("User ID not found.");
        }
        var user = await _userService.GetMyProfile(userId);
        if (user == null)
        {
            return BadRequest("User ID not found.");
        }

        return Ok(user);
    }

    [HttpPut("/update")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UserInfo userInfo){
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)){
            return BadRequest("User ID not found.");
        }
        var result = await _userService.UpdateUser(userId, userInfo);
        if (!result){
            return NotFound();
        }

        return Ok(new {message = "Update successfully."});
    }
}