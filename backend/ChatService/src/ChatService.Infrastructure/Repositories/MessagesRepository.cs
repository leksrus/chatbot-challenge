using AutoMapper;
using ChatService.Domain.Entities;
using ChatService.Domain.Repositories;
using ChatService.Infrastructure.Repositories.MongoModels;
using MongoDB.Driver;

namespace ChatService.Infrastructure.Repositories;

public class MessagesRepository : IMessagesRepository
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<MessageModel> _mongoMessagesCollection;
    private const string MessagesCollection = "messages";
    
    public MessagesRepository(IMongoDatabase mongoDatabase, IMapper mapper)
    {
        _mapper = mapper;
        _mongoMessagesCollection = mongoDatabase.GetCollection<MessageModel>(MessagesCollection);
    }
    
    public async Task<IEnumerable<Message>> GetLastMessagesAsync(string chatRoom, int messagesCount)
    {
        var filter = Builders<MessageModel>.Filter;
        var condition = filter.Eq("chat_room", chatRoom);
        var messagesModel = await _mongoMessagesCollection
            .Find(condition).SortByDescending(p => p.TimeStamp)
            .Skip(messagesCount)
            .ToListAsync();

        return _mapper.Map<IEnumerable<Message>>(messagesModel);
    }

    public async Task<Message> Add(Message message)
    {
        var mongoMessage = _mapper.Map<MessageModel>(message);

        await _mongoMessagesCollection.InsertOneAsync(mongoMessage);

        return message;
    }
}