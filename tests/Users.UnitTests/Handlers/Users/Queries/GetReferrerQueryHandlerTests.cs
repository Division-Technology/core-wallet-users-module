using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Users.Application.Handlers.Users.Queries.GetReferrer;
using Users.Domain.Entities.Users.Queries.GetReferrer;
using Users.Repositories.Users;
using Users.Data.Tables;
using Users.Application.Exceptions;
using Users.Domain.Enums;
using Xunit;

namespace Users.UnitTests.Handlers.Users.Queries
{
    public class GetReferrerQueryHandlerTests
    {
        private readonly Mock<IUsersRepository> _repoMock = new();
        private readonly GetReferrerQueryHandler _handler;

        public GetReferrerQueryHandlerTests()
        {
            _handler = new GetReferrerQueryHandler(_repoMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyResponse_WhenUserFound()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid() };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(new GetReferrerQuery { UserId = user.Id }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUserNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new GetReferrerQuery { UserId = Guid.NewGuid() }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenRepositoryThrows()
        {
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("DB error"));
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetReferrerQuery { UserId = Guid.NewGuid() }, CancellationToken.None));
        }
    }
} 