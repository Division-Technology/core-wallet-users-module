using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Users.Application.Handlers.Users.Commands;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Repositories.Users;
using Users.Data.Tables;
using Microsoft.Extensions.Logging;
using Users.Application.Exceptions;
using Users.Domain.Enums;
using Xunit;

namespace Users.UnitTests.Handlers.Users.Commands
{
    public class PatchUpdateUserCommandHandlerTests
    {
        private readonly Mock<IUsersRepository> _repoMock = new();
        private readonly Mock<ILogger<PatchUpdateUserCommandHandler>> _loggerMock = new();
        private readonly PatchUpdateUserCommandHandler _handler;

        public PatchUpdateUserCommandHandlerTests()
        {
            _handler = new PatchUpdateUserCommandHandler(_repoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateUser_WhenRequestIsValid()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), FirstName = "Old" };
            var request = new PatchUpdateUserCommand { Id = user.Id, FirstName = "New" };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _repoMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(user.Id, result.Id);
            _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var request = new PatchUpdateUserCommand { Id = Guid.NewGuid() };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnNoChanges_WhenNoFieldsAreUpdated()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), FirstName = "Same" };
            var request = new PatchUpdateUserCommand { Id = user.Id, FirstName = "Same" };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("No changes applied.", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenRepositoryThrows()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "Old" };
            var request = new PatchUpdateUserCommand { Id = user.Id, FirstName = "New" };
            _repoMock.Setup(r => r.GetAsync(It.IsAny<Func<User, bool>>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _repoMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("DB error"));

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            var request = new PatchUpdateUserCommand { Id = Guid.Empty };
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
} 