using aspdotnet_project.App.Show.Dtos;

namespace aspdotnet_project.App.Show.Services;

public interface IShowtimeService
{
    Task<List<Entities.Show>> Create(CreateShowtimeRequest createShowtimeRequest);
    Task<ShowtimeDetail> GetSeatByShowId(string showId);
}