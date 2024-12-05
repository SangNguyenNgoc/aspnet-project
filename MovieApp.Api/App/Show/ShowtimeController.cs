using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = "Admin")]
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
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _showtimeService.Delete(id));
    }
    
    [HttpPost("handwork")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateHandWork([FromBody] CreateShowtimeHandWork createShowtimeHandWork)
    {
        var result = await _showtimeService.CreateHandWork(createShowtimeHandWork);
        return Ok(result);
    }
    
}