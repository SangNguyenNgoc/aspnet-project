using AutoMapper;
using MovieApp.Application.Feature.Movie.Dtos;
using MovieApp.Domain.Movie.Entities;
using MovieApp.Infrastructure.S3;

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
            .ForMember(dest => dest.Status, otp => otp.MapFrom(scr => scr.Status))
            .ForMember(dest => dest.Formats, otp => otp.MapFrom(scr => scr.Formats))
            .ForMember(dest => dest.Genres, otp => otp.MapFrom(scr => scr.Genres));

        CreateMap<MovieStatus, MovieDetail.MovieStatusDto>();

        CreateMap<Format, FormatAndShow>()
            .ForMember(dest => dest.Shows, opt => opt.MapFrom(src => src.Shows
                .OrderBy(s => s.StartDate)
                .ThenBy(s => s.StartTime)
            ));

        CreateMap<StatusResponse, MovieStatus>().ReverseMap();

        CreateMap<MovieCreateRequest, Domain.Movie.Entities.Movie>()
            .ForMember(dest => dest.Poster, opt => opt.Ignore())
            .ForMember(dest => dest.HorizontalPoster, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Formats, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore());

        CreateMap<Domain.Movie.Entities.Movie, ManageMovie>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

    }
}