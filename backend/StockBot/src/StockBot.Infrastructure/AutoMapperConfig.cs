using StockBot.Infrastructure.Support.Helpers;

namespace StockBot.Infrastructure;

public static class AutoMapperConfig
{
    public static Type[] RegisterMappings()
    {
        return new[]
        {
            typeof(StoodQMessageDtoToChatMessage),
        };
    }
}