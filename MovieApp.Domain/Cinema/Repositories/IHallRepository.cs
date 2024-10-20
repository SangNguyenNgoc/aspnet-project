﻿using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Domain.Cinema.Repositories;

public interface IHallRepository
{
    Task<List<Hall>> GetHallsByDate(DateOnly date, string cinemaId);
}