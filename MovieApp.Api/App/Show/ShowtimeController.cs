﻿using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Show.Dtos;
using MovieApp.Application.Feature.Show.Services;

namespace MovieApp.Api.App.Show;

[Route("/api/v1/showtimes")]
[ApiController]
public class ShowtimeController : ControllerBase
{
    private readonly IShowtimeService _showtimeService;

    public ShowtimeController(IShowtimeService showtimeService)
    {
        _showtimeService = showtimeService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShowtimeRequest createShowtimeRequest)
    {
        await _showtimeService.Create(createShowtimeRequest);
        return Ok("success");
    }

    [HttpGet("{showId}/seats")]
    public async Task<IActionResult> GetSeatByShowId(string showId)
    {
        var result = await _showtimeService.GetSeatByShowId(showId);
        return Ok(result);
    }
}