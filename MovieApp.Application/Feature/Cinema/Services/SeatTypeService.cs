using AutoMapper;
using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Domain.Cinema.Repositories;

namespace MovieApp.Application.Feature.Cinema.Services;

public class SeatTypeService : ISeatTypeService
{
    private readonly ISeatTypeRepository _seatTypeRepository;
    private readonly IMapper _mapper;

    public SeatTypeService(ISeatTypeRepository seatTypeRepository, IMapper mapper)
    {
        _seatTypeRepository = seatTypeRepository;
        _mapper = mapper;
    }
    
    public async Task<List<SeatTypeResponse>> GetAll()
    {
        var seatTypes = await _seatTypeRepository.GetAll();
        return _mapper.Map<List<SeatTypeResponse>>(seatTypes);
    } 
}