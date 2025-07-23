using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Users.Application.Handlers.Users.Queries;
using Users.Domain.Entities.Users.Queries.Exists;
using Users.Repositories.Users;
using Users.Data.Tables;
using Users.Domain.Enums;
using Users.Application.Exceptions;
using Xunit;

namespace Users.UnitTests.Handlers.Users.Queries
{
    public class UserExistsQueryHandlerTests
    {
        private readonly Mock<IUsersRepository> _repoMock = new();
        private readonly UserExistsQueryHandler _handler;

        public UserExistsQueryHandlerTests()
        {
            _handler = new UserExistsQueryHandler(_repoMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnExists_WhenUserFoundById()
        {
            // Arrange
            var user = new User { Id = System.Guid.NewGuid() };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<System.Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(new UserExistsQuery(null, null, null, user.Id.ToString()), CancellationToken.None);

            // Assert
            Assert.True(result.Exists);
            Assert.Equal("UserId", result.FoundBy);
        }

        [Fact]
        public async Task Handle_ShouldReturnExists_WhenUserFoundByPhone()
        {
            // Arrange
            var user = new User { Id = System.Guid.NewGuid(), PhoneNumber = "+123" };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<System.Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(new UserExistsQuery(null, "+123", null, null), CancellationToken.None);

            // Assert
            Assert.True(result.Exists);
            Assert.Equal("PhoneNumber", result.FoundBy);
        }

        [Fact]
        public async Task Handle_ShouldReturnExists_WhenUserFoundByTelegramId()
        {
            // Arrange
            var user = new User { Id = System.Guid.NewGuid(), TelegramId = 123456 };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<System.Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(new UserExistsQuery("123456", null, null, null), CancellationToken.None);

            // Assert
            Assert.True(result.Exists);
            Assert.Equal("TelegramId", result.FoundBy);
        }

        [Fact]
        public async Task Handle_ShouldReturnExists_WhenUserFoundByChatId()
        {
            // Arrange
            var user = new User { Id = System.Guid.NewGuid(), ChatId = 654321 };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<System.Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(new UserExistsQuery(null, null, "654321", null), CancellationToken.None);

            // Assert
            Assert.True(result.Exists);
            Assert.Equal("ChatId", result.FoundBy);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotExists_WhenUserNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAsync(It.IsAny<System.Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            // Act
            var result = await _handler.Handle(new UserExistsQuery(null, null, null, null), CancellationToken.None);

            // Assert
            Assert.False(result.Exists);
            Assert.Null(result.FoundBy);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenRepositoryThrows()
        {
            _repoMock.Setup(r => r.GetAsync(It.IsAny<System.Func<User, bool>>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("DB error"));
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new UserExistsQuery(null, null, null, null), CancellationToken.None));
        }
    }
} 