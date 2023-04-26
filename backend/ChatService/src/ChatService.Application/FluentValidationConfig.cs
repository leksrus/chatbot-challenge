using System.Reflection;
using ChatService.Application.DTOs.Validators;

namespace ChatService.Application;

public class FluentValidationConfig
{
    public static IEnumerable<Assembly> RegisterValidations()
    {
        return new[]
        {
            typeof(ChatMessageDtoValidator).Assembly,
        };
    }
}