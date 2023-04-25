using AutoMapper;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Repositories.MongoModels;

namespace ChatService.Infrastructure.Support.Helpers;

public class UserModelToUser : Profile
{
    public UserModelToUser()
    {
        CreateMap<UserModel, User>();
    }
}