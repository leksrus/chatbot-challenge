using AutoMapper;
using StockBot.Application.DTOs;
using StockBot.Domain.Entities;

namespace StockBot.Infrastructure.Support.Helpers;

public class StoodQMessageDtoToChatMessage : Profile
{
    public StoodQMessageDtoToChatMessage()
    {
        CreateMap<StoodQMessageDto, ChatMessage>()
            .ForMember(dest => dest.Text,
                src =>
                    src.MapFrom(x => string.Concat(x.Symbol, " quote is $", x.Close, " per share")));
    }
}