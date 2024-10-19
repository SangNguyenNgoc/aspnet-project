using MovieApp.Domain.User.Entities;
using MovieApp.Identity.Dtos;

namespace MovieApp.Identity.Services;

public interface IAuthService
{
    Task<AuthResponse> Register(RegisterRequest request);

    Task<AuthResponse> Login(LoginRequest request);

    Task<string> GenerateToken(User user);
}