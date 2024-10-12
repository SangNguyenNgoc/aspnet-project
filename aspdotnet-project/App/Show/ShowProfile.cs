using aspdotnet_project.App.Show.Dtos;

namespace aspdotnet_project.App.Show;

public class ShowProfile : AutoMapper.Profile
{
    public ShowProfile()
    {
        CreateMap<Entities.Show, ShowResponseInFormat>();

        CreateMap<Entities.Show, ShowtimeDetail>()
            .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.Format));

        CreateMap<Movie.Entities.Movie, ShowtimeDetail.MovieDto>();
        
        CreateMap<Cinema.Entities.Hall, ShowtimeDetail.HallDto>()
            .ForMember(dest => dest.Cinema, opt => opt.MapFrom(src => src.Cinema))
            .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats));
        CreateMap<Cinema.Entities.Cinema, ShowtimeDetail.HallDto.CinemaDto>();
        CreateMap<Cinema.Entities.Seat, ShowtimeDetail.HallDto.SeatDto>();
    }
}