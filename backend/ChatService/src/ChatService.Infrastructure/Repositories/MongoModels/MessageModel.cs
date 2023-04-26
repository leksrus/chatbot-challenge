using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Infrastructure.Repositories.MongoModels;

public class MessageModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("user_name")]
    public string UserName { get; set; }

    [BsonElement("chat_room")]
    public string ChatRoom { get; set; }

    [BsonElement("time_stamp")]
    public string TimeStamp { get; set; }

    [BsonElement("text")]
    public string Text { get; set; }
}