namespace MovieApp.Domain.Cinema.Repositories;

public interface ICinemaRepository
{
    Task<List<Entities.Cinema>> GetCinemasByStatusOrderByCreateDate(string slug, DateOnly startDate, DateOnly endDate);
    Task<List<Domain.Cinema.Entities.Cinema>> GetCinemaAdmin();
    Task<string> Save(Entities.Cinema? cinema);
    Task<Domain.Cinema.Entities.Cinema?> GetById(string id);
    Task<Domain.Cinema.Entities.Cinema?> GetDetailById(string id);
    Task<List<Entities.Cinema>> GetCinemaByYear(int year);
    Task<Entities.Cinema?> UpdateStatus(Entities.Cinema? cinema);
    Task<Entities.Cinema?> GetBestCinema(DateTime from, DateTime to);
    Task<Entities.Cinema?> GetCinemaAdminById(string id);
}