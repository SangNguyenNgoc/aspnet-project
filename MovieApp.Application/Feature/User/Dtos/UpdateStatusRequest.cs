namespace MovieApp.Application.Feature.User.Dtos;

public class UpdateStatusRequest
{
    public string? UserId { get; set; }
    public int Status { get; set; }
}