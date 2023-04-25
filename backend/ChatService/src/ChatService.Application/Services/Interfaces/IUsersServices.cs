using ChatService.Application.DTOs;
using ChatService.Domain.Entities;

namespace ChatService.Application.Services.Interfaces;

public interface IUsersServices
{
    Task<UserDto> AddUserAsync(UserDto userDto);

    Task<bool> IsValidUserAsync(User user);
}