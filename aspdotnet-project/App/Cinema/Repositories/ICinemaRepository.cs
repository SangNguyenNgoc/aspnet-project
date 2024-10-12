namespace aspdotnet_project.App.Cinema.Repositories;

public interface ICinemaRepository
{
    Task<List<Entities.Cinema>> GetCinemasByStatusOrderByCreateDate(string slug, DateOnly startDate, DateOnly endDate);
}