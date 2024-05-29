using System.Net.Http.Json;
using System.Net;
using TwitterV2Processing.User.Models;
using Microsoft.AspNetCore.Mvc.Testing;

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

            // Act
            var response = await _client.GetAsync("/api/User");

            // Assert
            var users = await response.Content.ReadFromJsonAsync<List<UserModel>>();
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

            // Act
            var res = await _client.GetAsync("/api/User");

            // Assert
            var users = await res.Content.ReadFromJsonAsync<List<UserModel>>();
            // If any of the assertions fail, the test will still continue running and will report all the failed assertions at the end.
            // This allows you to get a complete picture of what went wrong in the test, rather than stopping at the first failure.
            // Useful when testing multiple components such as in an integration test.
            Assert.Multiple(() =>
            {
                Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(res.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(users, Is.Not.Null);
                Assert.That(users.Count, Is.GreaterThan(0));
            });
        }

        // Unhappy flow test
        [Test]
        public async Task GetAllUsers_ShouldReturn_EmptyResponse()
        {
            // Arrange

            // Act
            var res = await _client.GetAsync("/api/User");

            // Assert
            var users = await res.Content.ReadFromJsonAsync<List<UserModel>>();
            Assert.Multiple(() =>
            {
                Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(res.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(users.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public async Task DeleteUser_ShouldReturn_Unauthorized()
        {
            // Arrange
            var newUser = new UserModel("", "test1", "123", "user", 0, 0);
            var postRes = await _client.PostAsJsonAsync("/api/User", newUser);

            // Act
            var res = await _client.DeleteAsync($"/api/User?username={newUser.Username}");

            // Assert
            Assert.That(postRes.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}
