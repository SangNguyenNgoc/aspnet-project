using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Cinema.Repositories;

public interface ISeatRepository
{
    Task<List<Seat>> GetAllById(List<long> seatIds);
    void SaveAll(List<Seat> seats);
}