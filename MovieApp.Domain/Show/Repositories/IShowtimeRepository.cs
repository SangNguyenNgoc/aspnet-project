namespace MovieApp.Domain.Show.Repositories;

public interface IShowtimeRepository
{
    Task<List<Entities.Show>> FindByStartDateAndHallId(DateOnly starDate, string id);
    Task<List<Entities.Show>> Save(List<Entities.Show> show);
    Task<Entities.Show?> GetShowByIdCheckDateTime(string id);
    Task<Entities.Show?> GetShowByIdAfter7day(string id);
    Task<string> Delete(string id);
    Task<List<Domain.Show.Entities.Show>> CheckShowtimeByTime(DateTime date, long hallId);
}