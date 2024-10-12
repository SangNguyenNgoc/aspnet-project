using aspdotnet_project.App.Cinema.Entities;

namespace aspdotnet_project.App.Cinema.Repositories;

public interface IHallRepository
{
    Task<List<Hall>> GetHallsByDate(DateOnly date, string cinemaId);
}