using course_register.API.Dtos;

namespace aspdotnet_project.App.Auth;

public class AuthProfile : AutoMapper.Profile
{
    public AuthProfile()
    {
        CreateMap<User.Entities.User, AuthResponse>();
    }
}