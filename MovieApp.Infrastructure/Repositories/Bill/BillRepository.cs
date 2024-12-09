using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Bill.Repositories;
using MovieApp.Infrastructure.Context;
using MySqlConnector;

namespace MovieApp.Infrastructure.Repositories.Bill;

public class BillRepository : IBillRepository
{
    private readonly MyDbContext _context;

    public BillRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Bill.Entities.Bill> Create(Domain.Bill.Entities.Bill bill)
    {
        await _context.Bills.AddAsync(bill);
        await _context.SaveChangesAsync();
        return bill;
    }

    public async Task UpdateAsync(Domain.Bill.Entities.Bill bill)
    {
        _context.Bills.Update(bill);
        await _context.SaveChangesAsync();
    }

    public async Task<Domain.Bill.Entities.Bill?> GetByIdAsync(string billId)
    {
        return await _context.Bills.FirstOrDefaultAsync(b => b.Id == billId);
    }

    public async Task<ICollection<Domain.Bill.Entities.Bill>> GetByUserIdAsync(string userId)
    {
        return await _context.Bills
            .Where(b => b.User.Id == userId)
            .Include(b => b.Status)
            .Include(b => b.User)
            .Include(b => b.Tickets)
            .ThenInclude(t => t.Seat)
            .ThenInclude(s => s.Type)
            .ToListAsync();
    }

    public async Task<Domain.Bill.Entities.Bill?> GetBillDetailById(string billId)
    {
        return await _context.Bills
            .Where(b => b.Id == billId)
            .Include(b => b.Status)
            .Include(b => b.User)
            .Include(b => b.Tickets)
                .ThenInclude(t => t.Seat)
                .ThenInclude(s => s.Type)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<Domain.Bill.Entities.Bill>> GetAllBillsAreExpired()
    {
        return await _context.Bills
            .Where(b => b.Status.Id != 3)
            .Include(b => b.Status)
            .Include(b => b.User)
            .Include(b => b.Tickets)
            .ThenInclude(t => t.Seat)
            .ThenInclude(s => s.Type)
            .ToListAsync();
    }
    
    public async Task UpdateExpiredBillsStatus(DateTime dateTime)
    {
        const string sql = "UPDATE bills SET status_id = 3 WHERE expire_at < @dateTime AND status_id = 1";
        await _context.Database.ExecuteSqlRawAsync(sql, new MySqlParameter("@dateTime", dateTime));
    }

    public async Task<List<Domain.Bill.Entities.Bill>?> GetBillByYear(int year)
    {
        return await _context.Bills
            .Where(b => b.CreateAt.Year == year && b.Status.Id == 2)
            .Include(b => b.Tickets)
            .ToListAsync();
    }

    public async Task<List<Domain.Bill.Entities.Bill>?> GetBillByTime(DateTime from, DateTime to)
    {
        return await _context.Bills
            .Where(b => b.CreateAt >= from && b.CreateAt <= to && b.Status.Id == 2)
            .Include(b => b.Tickets)
            .ToListAsync();
    }
            
}