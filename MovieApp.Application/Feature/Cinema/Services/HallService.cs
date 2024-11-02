using AutoMapper;
using MovieApp.Application.Exception;
using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Domain.Cinema.Repositories;

namespace MovieApp.Application.Feature.Cinema.Services;

public class HallService : IHallService
{
    private readonly IMapper _mapper;
    private readonly IHallRepository _hallRepository;
    private readonly IHallStatusRepository _hallStatusRepository;
    private readonly ICinemaRepository _cinemaRepository;
    private readonly ISeatTypeRepository _seatTypeRepository;

    public HallService(IMapper mapper, IHallRepository hallRepository, IHallStatusRepository hallStatusRepository, ICinemaRepository cinemaRepository, ISeatTypeRepository seatTypeRepository)
    {
        _mapper = mapper;
        _hallRepository = hallRepository;
        _hallStatusRepository = hallStatusRepository;
        _cinemaRepository = cinemaRepository;
        _seatTypeRepository = seatTypeRepository;
    }
    
    public async Task<long> SaveHall(string cinemaId, HallCreated hallRequest)
    {
        var cinema = await _cinemaRepository.GetById(cinemaId) ?? 
                     throw new DataNotFoundException($"Cinema with id {cinemaId} not found");
        var status = await _hallStatusRepository.GetById(hallRequest.Status) ??
                     throw new DataNotFoundException($"Status with id {hallRequest.Status} not found");
        var type = await _seatTypeRepository.GetAll();
        if(hallRequest.Seats.Any(s => !type.Exists(t => t.Id == s.Type)))
            throw new DataNotFoundException("Seat type not found");
        var hall = _mapper.Map<Domain.Cinema.Entities.Hall>(hallRequest);
        hall.TotalSeats = hallRequest.Seats.Count;
        hall.Cinema = cinema;
        hall.Status = status;
        hall.Seats = hallRequest.Seats.Select(s =>
        {
            var seat = _mapper.Map<Domain.Cinema.Entities.Seat>(s);
            seat.Type = type.Find(t => t.Id == s.Type)!;
            return seat;
        }).ToList();
        return await _hallRepository.Save(hall);
    }
    
    public async Task<List<HallStatusResponse>> GetHallStatus()
    {
        var hallStatus = await _hallStatusRepository.GetAllHallStatus();
        return _mapper.Map<List<HallStatusResponse>>(hallStatus);
    }
    
    public async Task<HallResponse> GetHallById(long hallId)
    {
        var hall = await _hallRepository.GetHallById(hallId) ??
                   throw new DataNotFoundException($"Hall with id {hallId} not found");
        var hallMapper = _mapper.Map<HallResponse>(hall);
        hallMapper.Rows = hall.Seats.GroupBy(s => s.RowName).Select(s =>
        {
            var row = new HallResponse.RowDto 
            {
                RowName = s.Key,
                Seats = _mapper.Map<List<HallResponse.RowDto.SeatDto>>(s)
            };
            return row;
        }).ToList();
        return hallMapper;
    }
    
}