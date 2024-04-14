using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using System.Text.Json;
using TwitterV2Processing.User.Models;
using Xunit.Abstractions;

namespace TwitterV2Processing.User.Tests
{
    public class IntegrationTests : IClassFixture<MongoFixture>, IDisposable
    {
        private readonly MongoFixture _fixture;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper output;

        public IntegrationTests(MongoFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll<IMongoClient>();
                        services.AddSingleton<IMongoClient>(
                            (_) => _fixture.Client);
                    });
                });
            _client = appFactory.CreateClient();
            this.output = output;
        }

        public void Dispose()
        {
            var userDb = _fixture.Client.GetDatabase("usertests");
            var collection = userDb.GetCollection<UserModel>("users");
            collection.DeleteManyAsync(_ => true).Wait();
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnUsers()
        {
            //// Arrange
            //var userDb = _fixture.Client.GetDatabase("adminn");
            //userDb.CreateCollection("users");
            //var dbnames = _fixture.Client.ListDatabaseNames().ToList<string>();
            //foreach (var name in dbnames)
            //{
            //    output.WriteLine(name);
            //}
            //var collection = userDb.GetCollection<UserModel>("users");
            //await collection.InsertOneAsync(new UserModel("test3", "123", "user", 0, 0));
            //var documents = await collection.Find(_ => true).ToListAsync();
            //output.WriteLine(documents[0].Username);

            //// Act
            //var res = await _client.GetAsync("/api/Users");
            //res.EnsureSuccessStatusCode();
            //var content = await res.Content.ReadAsStringAsync();
            //var users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //});

            //// Assert
            //Assert.NotNull(users);
            //Assert.NotEmpty(users);
            //Assert.Single(users);
            //var resultItem = users.FirstOrDefault();
            //Assert.NotNull(resultItem);

            // Act
            var res = await _client.GetAsync("/api/User");
            res.EnsureSuccessStatusCode();
            var content = await res.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            // Assert
            Assert.NotNull(users);
            Assert.NotEmpty(users);
            Assert.Equal(2, users.Count);
            var resultItem = users.FirstOrDefault();
            Assert.NotNull(resultItem);
        }

        internal record User(string Username, string Password, string Role, int Followers, int Following);
    }
}