using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Users.Application.Handlers.Users.Queries.GetUserRegistrationStatus;
using Users.Domain.Entities.Users.Queries.GetRegistrationStatus;
using Users.Repositories.Users;
using Users.Data.Tables;
using Users.Domain.Enums;
using Users.Application.Exceptions;
using Xunit;

namespace Users.UnitTests.Handlers.Users.Queries
{
    public class GetUserRegistrationStatusQueryHandlerTests
    {
        private readonly Mock<IUsersRepository> _repoMock = new();
        private readonly GetUserRegistrationStatusQueryHandler _handler;

        public GetUserRegistrationStatusQueryHandlerTests()
        {
            _handler = new GetUserRegistrationStatusQueryHandler(_repoMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnStatus_WhenUserFound()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), RegistrationStatus = RegistrationStatus.Registered };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(new GetUserRegistrationStatusQuery { UserId = user.Id }, CancellationToken.None);

            // Assert
            Assert.Equal(user.RegistrationStatus, result.RegistrationStatus);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUserNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetUserRegistrationStatusQuery { UserId = Guid.NewGuid() }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenRepositoryThrows()
        {
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("DB error"));
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetUserRegistrationStatusQuery { UserId = Guid.NewGuid() }, CancellationToken.None));
        }
    }
} 