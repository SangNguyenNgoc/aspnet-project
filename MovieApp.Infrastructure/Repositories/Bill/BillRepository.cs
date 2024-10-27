using Microsoft.Data.SqlClient;
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
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<Domain.Bill.Entities.Bill>> GetAllBillsAreExpired(DateTime dateTime)
    {
        return await _context.Bills
            .Where(b => b.ExpireAt < dateTime && b.Status.Id == 1)
            .ToListAsync();
    }
    
    public async Task UpdateExpiredBillsStatus(DateTime dateTime)
    {
        const string sql = "UPDATE bills SET status_id = 3 WHERE expire_at < @dateTime AND status_id = 1";
        await _context.Database.ExecuteSqlRawAsync(sql, new MySqlParameter("@dateTime", dateTime));
    }

}