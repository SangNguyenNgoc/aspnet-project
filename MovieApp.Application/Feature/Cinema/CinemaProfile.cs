using MovieApp.Application.Feature.Cinema.Dtos;
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
    }
    
}