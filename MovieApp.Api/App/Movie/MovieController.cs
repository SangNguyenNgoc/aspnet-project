using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Application.Feature.Movie.Services;

namespace MovieApp.Api.App.Movie;

[ApiController]
[Route("/api/v1/movies")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("landing")]
    public async Task<IActionResult> GetMoviesToLanding()
    {
        return Ok(await _movieService.GetMovieToLanding());
    }

    [HttpGet("{slug}/shows")]
    public async Task<IActionResult> GetMovieDetail(string slug)
    {
        return Ok(await _movieService.GetMovieDetail(slug));
    }

    [HttpGet("coming-soon")]
    public async Task<IActionResult> GetMovieComingSoon([FromQuery] int page, [FromQuery] int perPage)
    {
        return Ok(await _movieService.GetMovieByStatus("coming-soon", page, perPage));
    }

    [HttpGet("showing-now")]
    public async Task<IActionResult> GetMoviesShowingNow([FromQuery] int page, [FromQuery] int perPage)
    {
        return Ok(await _movieService.GetMovieByStatus("showing-now", page, perPage));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromForm] MovieCreateRequest movieCreateRequest)
    {
        return Ok(await _movieService.CreateMovie(movieCreateRequest));
    }
    
    [HttpGet("status")]
    public async Task<IActionResult> GetAllStatus()
    {
        return Ok(await _movieService.GetAllStatus());
    }
}