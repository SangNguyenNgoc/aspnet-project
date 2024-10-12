using aspdotnet_project.App.Cinema.Dtos;
using aspdotnet_project.App.Cinema.Entities;
using aspdotnet_project.App.Movie.Dtos;
using aspdotnet_project.App.Movie.Entities;

namespace aspdotnet_project.App.Cinema;

public class CinemaProfile : AutoMapper.Profile
{
    public CinemaProfile()
    {
        CreateMap<Entities.Cinema, CinemaAndShow>()
            .ForMember(dest => 
                dest.Formats, opt => opt.Ignore());
        
        CreateMap<Location, LocationAndCinema>()
            .ForMember(dest => 
                dest.Cinemas, opt => opt.MapFrom(src => src.Cinemas.ToList())); // Map List of Cinemas

        // Mapping for CinemaDto
        CreateMap<Entities.Cinema, LocationAndCinema.CinemaDto>()
            .ForMember(dest => 
                dest.Movies, opt => opt.Ignore()); // Map distinct Movies

        // Mapping for MovieDto
        CreateMap<Movie.Entities.Movie, LocationAndCinema.CinemaDto.MovieDto>()
            .ForMember(dest => dest.Formats, opt => opt.Ignore());
    }
    
}