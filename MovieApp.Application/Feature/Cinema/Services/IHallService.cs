﻿using MovieApp.Application.Feature.Cinema.Dtos;

namespace MovieApp.Application.Feature.Cinema.Services;

public interface IHallService
{
    Task<long> SaveHall(string cinemaId, HallCreated hallRequest);
    Task<List<HallStatusResponse>> GetHallStatus();
    Task<HallResponse> GetHallById(long hallId);
    Task<HallResponse> UpdateHallStatus(long hallId, long statusId);
    Task<string> UpdateSeatStatus(long seatId);
}