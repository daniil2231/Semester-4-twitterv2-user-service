using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TwitterV2Processing.Authentication.Models
{
    public class UserAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
