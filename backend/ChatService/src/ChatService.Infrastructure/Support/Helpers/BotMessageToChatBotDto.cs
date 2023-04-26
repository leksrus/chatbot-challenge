using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Domain.Entities;

namespace ChatService.Infrastructure.Support.Helpers;

public class BotMessageToChatBotDto : Profile
{
    public BotMessageToChatBotDto()
    {
        CreateMap<BotMessage, ChatBotDto>();
    }
}