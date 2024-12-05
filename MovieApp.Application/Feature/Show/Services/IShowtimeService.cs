using MovieApp.Application.Feature.Show.Dtos;

namespace MovieApp.Application.Feature.Show.Services;

public interface IShowtimeService
{
    Task<List<ShowtimeDetail>> Create(CreateShowtimeRequest createShowtimeRequest);
    Task<ShowtimeDetail> GetSeatByShowId(string showId);
    Task<string> Delete(string id);
    Task<ShowtimeDetail> CreateHandWork(CreateShowtimeHandWork createShowtimeHandWork);
}