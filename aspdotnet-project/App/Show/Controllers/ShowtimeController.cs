using aspdotnet_project.App.Show.Dtos;
using aspdotnet_project.App.Show.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnet_project.App.Show.Controllers;

[Route("/api/v1/showtimes")]
[ApiController]
public class ShowtimeController: ControllerBase
{
    private readonly IShowtimeService _showtimeService;

    public ShowtimeController(IShowtimeService showtimeService)
    {
        _showtimeService = showtimeService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShowtimeRequest createShowtimeRequest)
    {
        var result = await _showtimeService.Create(createShowtimeRequest);
        return Ok("success");
    }
    
    [HttpGet("{showId}/seats")]
    public async Task<IActionResult> GetSeatByShowId(string showId)
    {
        var result = await _showtimeService.GetSeatByShowId(showId);
        return Ok(result);
    }
}