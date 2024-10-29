using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Domain.Movie.Repositories;

namespace MovieApp.Api.App.Movie;

[ApiController]
[Route("/api/v1/formats")]
public class FormatController: ControllerBase
{
    private readonly IFormatRepository _formatRepository;
    private readonly IMapper _mapper;

    public FormatController(IFormatRepository formatRepository, IMapper mapper)
    {
        _formatRepository = formatRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_mapper.Map<List<FormatResponse>>(await _formatRepository.GetAll()));
    }
}