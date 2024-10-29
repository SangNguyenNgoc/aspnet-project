using MovieApp.Application.Feature.Cinema.Dtos;

namespace MovieApp.Application.Feature.Cinema.Services;

public interface IHallService
{
    Task<long> SaveHall(string cinemaId, HallCreated hallRequest);
    Task<List<HallStatusResponse>> GetHallStatus();
}