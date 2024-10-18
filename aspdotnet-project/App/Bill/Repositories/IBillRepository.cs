namespace aspdotnet_project.App.Bill.Repositories;

public interface IBillRepository
{
    Task<entities.Bill> Create(entities.Bill bill);
}