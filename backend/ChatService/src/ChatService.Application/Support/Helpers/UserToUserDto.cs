using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Domain.Entities;

namespace ChatService.Application.Support.Helpers;

public class UserToUserDto : Profile
{
    public UserToUserDto()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Password,
                src => src.Ignore())
            .ForMember(dest => dest.RepeatPassword,
                src => src.Ignore());
    }
}