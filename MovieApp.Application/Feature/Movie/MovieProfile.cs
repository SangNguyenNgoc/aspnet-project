using AutoMapper;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Domain.Movie.Entities;

namespace MovieApp.Application.Feature.Movie;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        // Ánh xạ Movie sang MovieInfoLanding
        CreateMap<MovieStatus, StatusInfo>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));

        CreateMap<Domain.Movie.Entities.Movie, MovieInfoLanding>()
            .ForMember(dest => dest.Formats, opt => opt.MapFrom(src => src.Formats))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres));
        // Ánh xạ Format và Genre
        CreateMap<Format, FormatResponse>();
        CreateMap<Genre, GenreResponse>();

        CreateMap<Domain.Movie.Entities.Movie, MovieDetail>()
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