using aspdotnet_project.App.Auth.Dtos;

namespace aspdotnet_project.App.Auth.Services;

public interface IAuthService
{
    Task<AuthResponse> Register(RegisterRequest request);

    Task<AuthResponse> Login(LoginRequest request);

    Task<string> GenerateToken(User.Entities.User user);
}