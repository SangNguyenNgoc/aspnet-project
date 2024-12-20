﻿using MovieApp.Application.Feature.Cinema.Dtos;
using MovieApp.Application.Feature.Show.Dtos;
using MovieApp.Domain.Cinema.Entities;

namespace MovieApp.Application.Feature.Show;

public class ShowProfile : AutoMapper.Profile
{
    public ShowProfile()
    {
        CreateMap<Domain.Show.Entities.Show, ShowResponseInFormat>();

        CreateMap<Domain.Show.Entities.Show, ShowtimeDetail>()
            .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.Format));

        CreateMap<Domain.Movie.Entities.Movie, ShowtimeDetail.MovieDto>();
        
        CreateMap<Hall, HallResponse>()
            .ForMember(dest => dest.Cinema, opt => opt.MapFrom(src => src.Cinema))
            .ForMember(dest => dest.Rows, opt => opt.Ignore());
        CreateMap<Domain.Cinema.Entities.Cinema, HallResponse.CinemaDto>();
        CreateMap<Seat, HallResponse.RowDto.SeatDto>()
            .ForMember(dest => dest.SeatType, opt => opt.MapFrom(src => src.Type));
        CreateMap<Domain.Show.Entities.Show, ShowtimeDetailAdmin>()
            .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie))
            .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.Format));
    }
}