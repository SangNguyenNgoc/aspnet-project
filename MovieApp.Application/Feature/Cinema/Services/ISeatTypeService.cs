using MovieApp.Application.Feature.Cinema.Dtos;

namespace MovieApp.Application.Feature.Cinema.Services;

public interface ISeatTypeService
{
    Task<List<SeatTypeResponse>> GetAll();
}