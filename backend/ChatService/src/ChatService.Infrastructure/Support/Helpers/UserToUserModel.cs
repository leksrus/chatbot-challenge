using AutoMapper;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Repositories.MongoModels;

namespace ChatService.Infrastructure.Support.Helpers;

public class UserToUserModel : Profile
{
    public UserToUserModel()
    {
        CreateMap<User, UserModel>();
    }
}