﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Infrastructure.Repositories.MongoModels;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
}