namespace MovieApp.Application.Feature.Show.Dtos;

public class ShowResponseInFormat
{
    public string Id { get; set; }
    public int RunningTime { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public bool Status { get; set; }
}