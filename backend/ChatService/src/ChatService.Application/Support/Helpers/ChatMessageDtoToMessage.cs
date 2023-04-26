using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Domain.Entities;

namespace ChatService.Application.Support.Helpers;

public class ChatMessageDtoToMessage : Profile
{
    public ChatMessageDtoToMessage()
    {
        CreateMap<ChatMessageDto, Message>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.UserName))
            .ForMember(dest => dest.ChatRoom,
                src => src.MapFrom(x => x.ChatRoom))
            .ForMember(dest => dest.Text,
                src => src.MapFrom(x => x.Text))
            .ForMember(dest => dest.TimeStamp,
                src => src.MapFrom(_ => DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")));
    }
}