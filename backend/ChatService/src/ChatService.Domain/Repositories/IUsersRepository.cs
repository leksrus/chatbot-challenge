using ChatService.Domain.Entities;

namespace ChatService.Domain.Repositories;

public interface IUsersRepository
{
    Task<User> AddAsync(User user);

    Task<User> FindAsync(string userName);
}