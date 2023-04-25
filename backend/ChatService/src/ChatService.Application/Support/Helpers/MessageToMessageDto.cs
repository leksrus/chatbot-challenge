using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Domain.Entities;

namespace ChatService.Application.Support.Helpers;

public class MessageToMessageDto : Profile
{
    public MessageToMessageDto()
    {
        CreateMap<Message, MessageDto>();
    }
}