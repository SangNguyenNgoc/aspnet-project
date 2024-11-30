namespace MovieApp.Identity.Dtos;

public class AuthResponse
{
    public string? FullName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public IList<string>? Roles { get; set; }
}