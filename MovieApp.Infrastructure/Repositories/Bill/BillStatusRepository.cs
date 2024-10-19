using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Bill.Entities;
using MovieApp.Domain.Bill.Repositories;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Infrastructure.Repositories.Bill;

public class BillStatusRepository : IBillStatusRepository
{
    private readonly MyDbContext _context;

    public BillStatusRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<BillStatus?> GetBillStatusById(long id)
    {
        return await _context.BillStatus.FirstOrDefaultAsync(bs => bs.Id == id );
    }
}