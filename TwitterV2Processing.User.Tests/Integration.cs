using System.Net.Http.Json;
using System.Net;
using TwitterV2Processing.User.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using Microsoft.Extensions.Hosting;
using NUnit.Framework.Interfaces;

namespace TwitterV2Processing.User.Tests
{
    internal class Integration
    {
        private WebApplicationFactory<Program> _application;
        private HttpClient _client;

        [SetUp]
        public void SetUp() {
            _application = new UserWebAppFactory();
            _client = _application.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _application?.Dispose();
            _client?.Dispose();
        }

        [Test]
        public async Task Post_ShouldReturn_CreatedUser()
        {
            // Arrange
            UserModel newUser = new UserModel("", "test1", "123", "user", 0, 0);
            var createRes = await _client.PostAsJsonAsync("/api/User", newUser);
            createRes.EnsureSuccessStatusCode();

            var response = await _client.GetAsync("/api/User");

            // Act
            var users = await response.Content.ReadFromJsonAsync<List<UserModel>>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(users, Is.Not.Null);
                Assert.That(users.Count, Is.GreaterThan(0));
                Assert.That(users.Any(u => u.Username == newUser.Username));
            });
        }

        [Test]
        public async Task GetAllUsers_ShouldReturn_Users()
        {
            // Arrange
            var newUsers = new List<UserModel> {
                new UserModel("", "test1", "123", "user", 0, 0),
                new UserModel("", "test2", "123", "user", 0, 0)
            };

            foreach (UserModel user in newUsers)
            {
                var postRes = await _client.PostAsJsonAsync("/api/User", user);
                postRes.EnsureSuccessStatusCode();
            }

            var res = await _client.GetAsync("/api/User");

            // Act
            var users = await res.Content.ReadFromJsonAsync<List<UserModel>>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(res.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(users, Is.Not.Null);
                Assert.That(users.Count, Is.GreaterThan(0));
            });
        }
    }
}
