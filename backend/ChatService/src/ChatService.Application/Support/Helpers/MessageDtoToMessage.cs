using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Domain.Entities;

namespace ChatService.Application.Support.Helpers;

public class MessageDtoToMessage : Profile
{
    public MessageDtoToMessage()
    {
        CreateMap<MessageDto, Message>();
    }
}