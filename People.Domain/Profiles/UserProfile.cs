using AutoMapper;
using People.Domain.Commands.Autentication;
using People.Domain.DTOs;
using People.Domain.Entities;

namespace People.Domain.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, CreateUserCommand>();
        CreateMap<CreateUserCommand, User>();
        CreateMap<User, ReadUserDto>();
        CreateMap<LoginUserDto, LoginUserCommand>();
        CreateMap<LoginUserCommand, User>();
    }
}
