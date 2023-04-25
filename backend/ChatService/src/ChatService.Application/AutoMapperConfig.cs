using ChatService.Application.Support.Helpers;

namespace ChatService.Application;

public static class AutoMapperConfig
{
    public static Type[] RegisterMappings()
    {
        return new[]
        {
            typeof(MessageDtoToMessage),
            typeof(MessageToMessageDto),
            typeof(UserToUserDto),
            typeof(UserDtoToUser)
        };
    }
}