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
            var user = new User { Id = Guid.NewGuid() };
            var response = new GetUserQueryResponse { Id = user.Id };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<GetUserQueryResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(new GetUserQuery { Id = user.Id }, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUserNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetUserQuery { Id = Guid.NewGuid() }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserFoundByPhone()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), PhoneNumber = "+123" };
            var response = new GetUserQueryResponse { Id = user.Id };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<GetUserQueryResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(new GetUserQuery { PhoneNumber = "+123" }, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenRepositoryThrows()
        {
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("DB error"));
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetUserQuery { Id = Guid.NewGuid() }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenIdAndPhoneAreNull()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(new GetUserQuery(), CancellationToken.None));
        }
    }
} 