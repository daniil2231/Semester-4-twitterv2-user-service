using MongoDB.Bson;
using System;

namespace TwitterV2Processing.User.Models
{
    public class UserModel
    {
        public UserModel(string username, string password, string role, int followers, int following) {
            Username = username;
            Password = password;
            Role = role;
            Followers = followers;
            Following = following;
        }

        public ObjectId Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string Role { get; init; }
        public int Followers { get; init; }
        public int Following { get; init; }
    }
}
