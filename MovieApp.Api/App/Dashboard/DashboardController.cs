using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Dashboard.Services;

namespace MovieApp.Api.App.Dashboard;

[ApiController]
[Route("/api/v1/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;
    
    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDashboard(int year)
    {
        return Ok(await _dashboardService.GetDashboard(year));
    }
    
    [HttpGet("movie")]
    public async Task<IActionResult> StatisticMovie(int month, int year)
    {
        return Ok(await _dashboardService.StatisticMovie(month, year));
    }
    
    [HttpGet("cinema")]
    public async Task<IActionResult> StatisticCinema(int month, int year)
    {
        return Ok(await _dashboardService.StatisticCinema(month, year));
    }
}