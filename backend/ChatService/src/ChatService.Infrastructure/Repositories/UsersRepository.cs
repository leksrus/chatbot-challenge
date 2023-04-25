using AutoMapper;
using ChatService.Domain.Entities;
using ChatService.Domain.Repositories;
using ChatService.Infrastructure.Repositories.MongoModels;
using MongoDB.Driver;

namespace ChatService.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<UserModel> _mongoUsersCollection;
    private const string UserCollection = "users";

    public UsersRepository(IMongoDatabase mongoDatabase, IMapper mapper)
    {
        _mapper = mapper;
        _mongoUsersCollection = mongoDatabase.GetCollection<UserModel>(UserCollection);
    }
    
    public async Task<User> AddAsync(User user)
    {
        var mongoUser = _mapper.Map<UserModel>(user);
        
        await _mongoUsersCollection.InsertOneAsync(mongoUser);

        return user;
    }

    public async Task<User> FindAsync(string userName)
    {
        var result = await _mongoUsersCollection.FindAsync(_ => _.Name == userName);

        var mongoUser = await result.SingleOrDefaultAsync();

        return _mapper.Map<User>(mongoUser);
    }
}