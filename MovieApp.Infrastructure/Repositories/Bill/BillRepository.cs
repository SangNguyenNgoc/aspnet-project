using MovieApp.Domain.Bill.Repositories;
using MovieApp.Infrastructure.Context;

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
}