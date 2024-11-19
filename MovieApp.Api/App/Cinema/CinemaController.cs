using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Cinema.Services;

namespace MovieApp.Api.App.Cinema;

[ApiController]
[Route("/api/v1/cinemas")]
public class CinemaController : ControllerBase
{
    private readonly ICinemaService _cinemaService;
    private readonly IHallService _hallService;

    public CinemaController(ICinemaService cinemaService, IHallService hallService)
    {
        _cinemaService = cinemaService;
        _hallService = hallService;
    }

    [HttpGet(template:"shows")]
    public async Task<IActionResult> GetCinemasAndShows()
    {
        return Ok(await _cinemaService.GetAllCinemas());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCinemas()
    {
        return Ok(await _cinemaService.GetCinemaAdmin());
    }
    
    [HttpGet(template:"status")]
    public async Task<IActionResult> GetCinemaStatus()
    {
        return Ok(await _cinemaService.GetAllStatus());
    }
    
    [HttpGet(template:"location")]
    public async Task<IActionResult> GetCinemaLocation()
    {
        return Ok(await _cinemaService.GetAllLocation());
    }
    
    [HttpPost]
    public async Task<IActionResult> SaveCinema([FromBody] CinemaCreated cinemaRequest)
    {
        return Ok(await _cinemaService.SaveCinema(cinemaRequest));
    }
    
    [HttpGet("hall/status")]
    public async Task<IActionResult> GetHallStatus()
    {
        return Ok(await _hallService.GetHallStatus());
    }
    
    [HttpPost("{cinemaId}/hall")]
    public async Task<IActionResult> SaveHall(string cinemaId, [FromBody] HallCreated hallRequest)
    {
        return Ok(await _hallService.SaveHall(cinemaId, hallRequest));
    }
    
    [HttpGet("{cinemaId}")]
    public async Task<IActionResult> GetCinemaDetail(string cinemaId)
    {
        return Ok(await _cinemaService.GetCinemaDetail(cinemaId));
    }
    
    [HttpGet("hall/{hallId}")]
    public async Task<IActionResult> GetHallDetail(long hallId)
    {
        return Ok(await _hallService.GetHallById(hallId));
    }
    
    [HttpPut("hall/{hallId}/status")]
    public async Task<IActionResult> UpdateHallStatus(long hallId, [FromBody] HallStatusRequest statusRequest)
    {
        return Ok(await _hallService.UpdateHallStatus(hallId, statusRequest.StatusId));
    }
    
    [HttpPut("{cinemaId}/status")]
    public async Task<IActionResult> UpdateCinemaStatus(string cinemaId)
    {
        return Ok(await _cinemaService.UpdateCinemaStatus(cinemaId));
    }
    
    [HttpPut("hall/seat/{seatId}")]
    public async Task<IActionResult> UpdateSeatStatus(long seatId)
    {
        return Ok(await _hallService.UpdateSeatStatus(seatId));
    }
}