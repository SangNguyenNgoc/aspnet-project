namespace MovieApp.Domain.Cinema.Repositories;

public interface ICinemaRepository
{
    Task<List<Entities.Cinema>> GetCinemasByStatusOrderByCreateDate(string slug, DateOnly startDate, DateOnly endDate);
    Task<List<Domain.Cinema.Entities.Cinema>> GetCinemaAdmin();
    Task<string> Save(Domain.Cinema.Entities.Cinema cinema);
    Task<Domain.Cinema.Entities.Cinema?> GetById(string id);
    Task<Domain.Cinema.Entities.Cinema?> GetDetailById(string id);
}