using aspdotnet_project.App.Movie.Dtos;
using aspdotnet_project.App.Movie.Entities;

namespace aspdotnet_project.App.Movie;

public class MovieProfile : AutoMapper.Profile
{
    public MovieProfile()
    {
        
        // Ánh xạ Movie sang MovieInfoLanding
        CreateMap<MovieStatus, StatusInfo>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));

        CreateMap<Entities.Movie, MovieInfoLanding>()
            .ForMember(dest => dest.Formats, opt => opt.MapFrom(src => src.Formats))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres));
        // Ánh xạ Format và Genre
        CreateMap<Format, FormatResponse>();
        CreateMap<Genre, GenreResponse>();

        CreateMap<Entities.Movie, MovieDetail>()
            .ForMember(dest => dest.Cinemas, otp => otp.Ignore())
            .ForMember(dest => dest.Status, otp => otp.MapFrom(scr => scr.Status));

        CreateMap<MovieStatus, MovieDetail.MovieStatusDto>();
        
        CreateMap<Format, FormatAndShow>()
            .ForMember(dest => dest.Shows, opt => opt.MapFrom(src => src.Shows
                .OrderBy(s => s.StartDate)
                .ThenBy(s => s.StartTime)
            ));

    }
    
}