using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Domain.Entities;

namespace ChatService.Application.Support.Helpers;

public class UserDtoToUser : Profile
{
    public UserDtoToUser()
    {
        CreateMap<UserAuthDto, User>();
    }
}