using aspdotnet_project.Context;

namespace aspdotnet_project.App.Bill.Repositories;

public class BillRepository : IBillRepository
{
    private readonly MyDbContext _context;

    public BillRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<entities.Bill> Create(entities.Bill bill)
    {
        await _context.Bills.AddAsync(bill);
        await _context.SaveChangesAsync();
        return bill;
    }
}