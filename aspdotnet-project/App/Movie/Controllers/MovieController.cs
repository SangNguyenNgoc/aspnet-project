using aspdotnet_project.App.Movie.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnet_project.App.Movie.Controllers;

[Route("/api/v1/movies")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("/landing")]
    public async Task<IActionResult> GetMoviesToLanding()
    {
        return Ok( await _movieService.GetMovieToLanding());
    }

    [HttpGet("/{slug}/shows")]
    public async Task<IActionResult> GetMovieDetail(string slug)
    {
        return Ok(await _movieService.GetMovieDetail(slug));
    }
    
    [HttpGet("/coming-soon")]
    public async Task<IActionResult> GetMovieComingSoon([FromQuery] int page, [FromQuery] int perPage)
    {
        return Ok(await _movieService.GetMovieByStatus("coming-soon", page, perPage));
    }
    
    [HttpGet("/showing-now")]
    public async Task<IActionResult> GetMovieshowingNow([FromQuery] int page, [FromQuery] int perPage)
    {
        return Ok(await _movieService.GetMovieByStatus("showing-now", page, perPage));
    }
}