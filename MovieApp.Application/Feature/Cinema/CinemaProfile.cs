using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Show.Dtos;
using MovieApp.Domain.Cinema.Entities;
using MovieApp.Domain.Movie.Entities;

namespace MovieApp.Application.Feature.Cinema;

public class CinemaProfile : AutoMapper.Profile
{
    public CinemaProfile()
    {
        CreateMap<Domain.Cinema.Entities.Cinema, CinemaAndShow>()
            .ForMember(dest => 
                dest.Formats, opt => opt.Ignore());
        
        CreateMap<Location, LocationAndCinema>()
            .ForMember(dest => 
                dest.Cinemas, opt => opt.MapFrom(src => src.Cinemas.ToList())); // Map List of Cinemas

        // Mapping for CinemaDto
        CreateMap<Domain.Cinema.Entities.Cinema, LocationAndCinema.CinemaDto>()
            .ForMember(dest => 
                dest.Movies, opt => opt.Ignore()); // Map distinct Movies

        // Mapping for MovieDto
        CreateMap<Domain.Movie.Entities.Movie, LocationAndCinema.CinemaDto.MovieDto>()
            .ForMember(dest => dest.Formats, opt => opt.Ignore());
        
        CreateMap<Domain.Cinema.Entities.Cinema, CinemaDetail>()
            .ForMember(dest => 
                dest.location, opt => opt.MapFrom(src => src.Location.Name)); // Map Location Name

        CreateMap<SeatType, SeatTypeResponse>();

        CreateMap<CinemaStatus, CinemaStatusResponse>();

        CreateMap<Location, LocationResponse>();
        
        CreateMap<CinemaCreated, Domain.Cinema.Entities.Cinema>()
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Location, opt => opt.Ignore())
            .ForMember(dest => dest.Slug , opt => opt.Ignore());

        CreateMap<HallStatus, HallStatusResponse>();

        CreateMap<HallCreated, Hall>()
            .ForMember(dest => dest.Seats, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());

        CreateMap<HallCreated.SeatCreated, Seat>()
            .ForMember(dest => dest.Type, opt => opt.Ignore());
        
        CreateMap<Domain.Cinema.Entities.Cinema, CinemaDetailManage>()
            .ForMember(dest => dest.location, opt => opt.MapFrom(src => src.Location.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Halls, opt => opt.MapFrom(src => src.Halls.ToList()));
        
        CreateMap<Hall, CinemaDetailManage.HallDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

        CreateMap<Domain.Cinema.Entities.Cinema, CinemaAdminDetail>()
            .ForMember(dest => dest.Halls, opt => opt.MapFrom(src => src.Halls.ToList()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Name));
        
        CreateMap<Hall, CinemaAdminDetail.HallAdmin>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Showtimes, opt => opt.MapFrom(src => src.Shows.ToList()));
    }
    
}