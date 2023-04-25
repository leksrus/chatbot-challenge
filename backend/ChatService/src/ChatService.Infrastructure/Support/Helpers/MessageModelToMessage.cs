using AutoMapper;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Repositories.MongoModels;

namespace ChatService.Infrastructure.Support.Helpers;

public class MessageModelToMessage : Profile
{
    public MessageModelToMessage()
    {
        CreateMap<MessageModel, Message>();
    }
}