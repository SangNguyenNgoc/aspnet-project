﻿namespace MovieApp.Domain.Bill.Repositories;

public interface IBillRepository
{
    Task<Entities.Bill> Create(Entities.Bill bill);
    
    Task UpdateAsync(Entities.Bill bill);

    Task<Entities.Bill?> GetByIdAsync(string billId);
}