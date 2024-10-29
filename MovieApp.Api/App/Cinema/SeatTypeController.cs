using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Cinema.Services;

namespace MovieApp.Api.App.Cinema;

[ApiController]
[Route("/api/v1/seat-types")]
public class SeatTypeController : ControllerBase
{
    private readonly ISeatTypeService _seatTypeService;

    public SeatTypeController(ISeatTypeService seatTypeService)
    {
        _seatTypeService = seatTypeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var seatTypes = await _seatTypeService.GetAll();
        return Ok(seatTypes);
    }
}