namespace MovieApp.Domain.Bill.Repositories;

public interface IBillRepository
{
    Task<Entities.Bill> Create(Entities.Bill bill);
}