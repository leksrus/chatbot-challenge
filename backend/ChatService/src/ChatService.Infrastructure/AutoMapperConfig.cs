using ChatService.Infrastructure.Support.Helpers;

namespace ChatService.Infrastructure;

public static class AutoMapperConfig
{
    public static Type[] RegisterMappings()
    {
        return new[]
        {
            typeof(UserToUserModel),
            typeof(UserModelToUser),
            typeof(MessageModelToMessage),
            typeof(MessageToMessageModel),
            typeof(BotMessageToChatBotDto)
        };
    }
}