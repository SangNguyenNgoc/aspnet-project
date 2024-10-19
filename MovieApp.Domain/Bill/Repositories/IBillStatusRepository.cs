using MovieApp.Domain.Bill.Entities;

namespace MovieApp.Domain.Bill.Repositories;

public interface IBillStatusRepository
{
    Task<BillStatus?> GetBillStatusById(long id);
}