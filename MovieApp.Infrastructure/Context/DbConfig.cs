namespace MovieApp.Infrastructure.Context;

public class DbConfig
{
    public required string Server { get; set; }
    public required string Database { get; set; }
    public required string User { get; set; }
    public required string Password { get; set; }
}