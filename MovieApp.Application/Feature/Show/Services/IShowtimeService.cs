using MovieApp.Application.Feature.Show.Dtos;

namespace MovieApp.Application.Feature.Show.Services;

public interface IShowtimeService
{
    Task<List<Domain.Show.Entities.Show>> Create(CreateShowtimeRequest createShowtimeRequest);
    Task<ShowtimeDetail> GetSeatByShowId(string showId);
}