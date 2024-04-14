using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TwitterV2Processing.User.Models
{
    public class UserModel
    {
        public UserModel(string id, string username, string password, string role, int followers, int following)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
            Followers = followers;
            Following = following;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string Role { get; init; }
        public int Followers { get; init; }
        public int Following { get; init; }
    }
}
