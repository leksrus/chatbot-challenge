using ChatService.Application.DTOs;

namespace ChatService.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthDto> GetTokenAsync(UserAuthDto userAuthDto);
}