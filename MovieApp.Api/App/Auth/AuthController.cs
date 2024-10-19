using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Identity.Services;
using LoginRequest = MovieApp.Identity.Dtos.LoginRequest;
using RegisterRequest = MovieApp.Identity.Dtos.RegisterRequest;

namespace MovieApp.Api.App.Auth;

[Route("/api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.Register(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.Login(request);
        return Ok(result);
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public string ForAdmin()
    {
        return "Admin";
    }

    [HttpGet("user")]
    [Authorize(Roles = "User")]
    public string ForUser()
    {
        return "User";
    }

    [HttpGet("any")]
    [Authorize]
    public string ForAnyAuthenticate()
    {
        return "Any request which authenticated";
    }

    [HttpGet("permit")]
    public string ForPermitAll()
    {
        return "Any request is permitted";
    }
}