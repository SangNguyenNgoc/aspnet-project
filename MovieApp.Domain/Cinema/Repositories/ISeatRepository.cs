using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Cinema.Repositories;

public interface ISeatRepository
{
    Task<List<Seat>> GetAllById(List<long> seatIds);
    void SaveAll(List<Seat> seats);
    Task<Seat?> UpdateStatus(Seat? seat);
    Task<Seat?> GetById(long seatId);
}