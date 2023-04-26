using FluentValidation;

namespace ChatService.Application.DTOs.Validators;

public class ChatMessageDtoValidator : AbstractValidator<ChatMessageDto>
{
    public ChatMessageDtoValidator()
    {
        RuleFor(m => m.UserName)
            .Null();
    }
}