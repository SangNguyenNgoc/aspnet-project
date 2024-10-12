using aspdotnet_project.App.Cinema.Dtos;
using aspdotnet_project.App.Cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnet_project.App.Cinema.Controllers;

[Route("/api/v1/cinemas")]
[ApiController]
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