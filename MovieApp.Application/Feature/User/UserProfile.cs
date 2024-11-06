using AutoMapper;
using MovieApp.Application.Feature.User.Dtos;
using MovieApp.Domain.User.Entities;

namespace MovieApp.Application.Feature.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.User.Entities.User, UserInfo>();
    }
}