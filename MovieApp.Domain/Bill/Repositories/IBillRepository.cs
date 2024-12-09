namespace MovieApp.Domain.Bill.Repositories;

public interface IBillRepository
{
    Task<Entities.Bill> Create(Entities.Bill bill);
    
    Task UpdateAsync(Entities.Bill bill);

    Task<Entities.Bill?> GetByIdAsync(string billId);

    Task<ICollection<Entities.Bill>> GetByUserIdAsync(string userId);

    Task<Entities.Bill?> GetBillDetailById(string billId);

    Task<ICollection<Entities.Bill>> GetAllBillsAreExpired();
    
    Task UpdateExpiredBillsStatus(DateTime dateTime);
    
    Task<List<Entities.Bill>?> GetBillByYear(int year);
    
    Task<List<Entities.Bill>?> GetBillByTime(DateTime from, DateTime to);

}