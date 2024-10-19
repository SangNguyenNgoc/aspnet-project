using AutoMapper;
using MovieApp.Identity.Dtos;

namespace MovieApp.Identity;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<Domain.User.Entities.User, AuthResponse>();
    }
}