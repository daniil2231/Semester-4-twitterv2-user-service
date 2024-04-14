using FluentAssertions;
using Moq;
using System.Collections.Generic;
using TwitterV2Processing.User.Controllers;
using TwitterV2Processing.User.Models;
using TwitterV2Processing.User.Persistence;
using Xunit.Abstractions;

namespace TwitterV2Processing.User.Tests
{
    public class UnitTests
    {
        private Mock<IUserRepository> _mockRepo;
        private UserController _userController;
        private readonly ITestOutputHelper output;

        public UnitTests(ITestOutputHelper output) {
            _mockRepo = new Mock<IUserRepository>();
            _userController = new UserController(_mockRepo.Object);
            this.output = output;
        }

        [Fact]
        public async void GetAllUsers_ReturnsExactNumberOfUsers() {
            // Arrange
            List<UserModel> userModels = new List<UserModel> {
                { new UserModel("test3", "123", "user", 0, 0) },
                { new UserModel("test4", "123", "user", 0, 0) }
            };
            _mockRepo.Setup(repo => repo.GetAllUsers<UserModel>("admin", "users"))
                .Returns(Task.FromResult(userModels));

            // Act
            var result = await _userController.GetAll();
            output.WriteLine(result.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userModels.Count, result.Count());
        }
    }
}
