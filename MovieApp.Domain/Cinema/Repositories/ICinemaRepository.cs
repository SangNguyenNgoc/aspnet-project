namespace MovieApp.Domain.Cinema.Repositories;

public interface ICinemaRepository
{
    Task<List<Entities.Cinema>> GetCinemasByStatusOrderByCreateDate(string slug, DateOnly startDate, DateOnly endDate);
}