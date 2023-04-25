using AutoMapper;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Repositories.MongoModels;

namespace ChatService.Infrastructure.Support.Helpers;

public class MessageToMessageModel : Profile
{
    public MessageToMessageModel()
    {
        CreateMap<Message, MessageModel>();
    }
}