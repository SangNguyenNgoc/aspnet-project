namespace MovieApp.Domain.Show.Repositories;

public interface IShowtimeRepository
{
    Task<List<Entities.Show>> FindByStartDateAndHallId(DateOnly starDate, string id);
    Task<List<Entities.Show>> Save(List<Entities.Show> show);
    Task<Entities.Show?> GetShowByIdCheckDateTime(string id);
}