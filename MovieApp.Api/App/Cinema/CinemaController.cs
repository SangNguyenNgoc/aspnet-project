using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Cinema.Services;

namespace MovieApp.Api.App.Cinema;

[ApiController]
[Route("/api/v1/cinemas")]
public class CinemaController : ControllerBase
{
    private readonly ICinemaService _cinemaService;

    public CinemaController(ICinemaService cinemaService)
    {
        _cinemaService = cinemaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCinemas()
    {
        return Ok(await _cinemaService.GetAllCinemas());
    }
}