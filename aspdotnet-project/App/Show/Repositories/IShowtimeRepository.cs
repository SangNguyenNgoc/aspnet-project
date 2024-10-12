using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace aspdotnet_project.App.Show.Repositories;

public interface IShowtimeRepository
{
    Task<List<Entities.Show>> FindByStartDateAndHallId(DateOnly starDate, string id);
    
    Task<List<Entities.Show>> Save(List<Entities.Show> show);
    Task<Entities.Show?> GetShowByIdCheckDateTime(string id);
}