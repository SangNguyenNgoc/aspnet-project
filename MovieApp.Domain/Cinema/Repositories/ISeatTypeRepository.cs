using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Cinema.Repositories;

public interface ISeatTypeRepository
{
    Task<List<SeatType>> GetAll();
}