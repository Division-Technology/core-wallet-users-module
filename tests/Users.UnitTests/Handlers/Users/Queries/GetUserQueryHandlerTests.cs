using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Users.Application.Handlers.Users.Queries.GetUser;
using Users.Domain.Entities.Users.Queries.GetUser;
using Users.Repositories.Users;
using Users.Data.Tables;
using Users.Application.Exceptions;
using Users.Domain.Enums;
using Xunit;

namespace Users.UnitTests.Handlers.Users.Queries
{
    public class GetUserQueryHandlerTests
    {
        private readonly Mock<IUsersRepository> _repoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly GetUserQueryHandler _handler;

        public GetUserQueryHandlerTests()
        {
            _handler = new GetUserQueryHandler(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserFoundById()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, FirstName = "Test", LastName = "User" };
            var response = new GetUserQueryResponse { Id = user.Id };
            
            _repoMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<GetUserQueryResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(new GetUserQuery { Id = userId }, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, result.Id);
            _repoMock.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUserNotFoundById()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetUserQuery { Id = userId }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserFoundByTelegramId()
        {
            // Arrange
            var telegramId = 123456789L;
            var user = new User { Id = Guid.NewGuid(), TelegramId = telegramId, FirstName = "Test", LastName = "User" };
            var response = new GetUserQueryResponse { Id = user.Id };
            
            _repoMock.Setup(r => r.GetByTelegramIdAsync(telegramId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<GetUserQueryResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(new GetUserQuery { TelegramId = telegramId }, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, result.Id);
            _repoMock.Verify(r => r.GetByTelegramIdAsync(telegramId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserFoundByChatId()
        {
            // Arrange
            var chatId = 987654321L;
            var user = new User { Id = Guid.NewGuid(), ChatId = chatId, FirstName = "Test", LastName = "User" };
            var response = new GetUserQueryResponse { Id = user.Id };
            
            _repoMock.Setup(r => r.GetByChatIdAsync(chatId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<GetUserQueryResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(new GetUserQuery { ChatId = chatId }, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, result.Id);
            _repoMock.Verify(r => r.GetByChatIdAsync(chatId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserFoundByPhone()
        {
            // Arrange
            var phoneNumber = "+1234567890";
            var user = new User { Id = Guid.NewGuid(), PhoneNumber = phoneNumber, FirstName = "Test", LastName = "User" };
            var response = new GetUserQueryResponse { Id = user.Id };
            
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<GetUserQueryResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(new GetUserQuery { PhoneNumber = phoneNumber }, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, result.Id);
            _repoMock.Verify(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldPrioritizeIdOverTelegramId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var telegramId = 123456789L;
            var user = new User { Id = userId, TelegramId = telegramId, FirstName = "Test", LastName = "User" };
            var response = new GetUserQueryResponse { Id = user.Id };
            
            _repoMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<GetUserQueryResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(new GetUserQuery { Id = userId, TelegramId = telegramId }, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, result.Id);
            _repoMock.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(r => r.GetByTelegramIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenNoUserFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetUserQuery { Id = userId }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenNoIdentifiersProvided()
        {
            // Arrange
            var query = new GetUserQuery { Id = null, TelegramId = null, ChatId = null, PhoneNumber = null };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenRepositoryThrows()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("DB error"));
            
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetUserQuery { Id = userId }, CancellationToken.None));
        }
    }
} 