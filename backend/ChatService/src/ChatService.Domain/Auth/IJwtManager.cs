using ChatService.Domain.Entities;

namespace ChatService.Domain.Auth;

public interface IJwtManager
{
    string GetJwt(User user);
}