using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Domain.Movie.Repositories;

namespace MovieApp.Api.App.Movie;

[ApiController]
[Route("/api/v1/genres")]
public class GenreController: ControllerBase
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreController(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_mapper.Map<List<GenreResponse>>(await _genreRepository.GetAll()));
    }
}