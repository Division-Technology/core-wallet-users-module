using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Users.Application.Handlers.Users.Commands;
using Users.Repositories.Users;
using Users.Data.Tables;
using Users.Domain.Enums;
using Users.Application.Exceptions;
using Users.Domain.Entities.Users.Commands.Create;
using Xunit;

namespace Users.UnitTests.Handlers.Users.Commands
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUsersRepository> _repoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _handler = new CreateUserCommandHandler(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateUserAndReturnResponse_WhenRequestIsValid()
        {
            // Arrange
            var request = new CreateUserCommandRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Language = "en",
                PhoneNumber = "+1234567890",
                RegistrationStatus = RegistrationStatus.Registered,
                IsBlocked = false,
                HasVehicle = true,
                TelegramId = 123456,
                ChatId = 654321,
                Username = "johndoe"
            };
            var user = new User { Id = Guid.NewGuid(), FirstName = "John" };
            var response = new CreateUserCommandResponse { Id = user.Id };

            _mapperMock.Setup(m => m.Map<User>(request)).Returns(user);
            _repoMock.Setup(r => r.AddAndSaveAsync(user)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<CreateUserCommandResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _repoMock.Verify(r => r.AddAndSaveAsync(user), Times.Once);
            _mapperMock.Verify(m => m.Map<User>(request), Times.Once);
            _mapperMock.Verify(m => m.Map<CreateUserCommandResponse>(user), Times.Once);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var handler = new CreateUserCommandHandler(_repoMock.Object, _mapperMock.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedResponse()
        {
            // Arrange
            var request = new CreateUserCommandRequest { FirstName = "Jane" };
            var user = new User { Id = Guid.NewGuid(), FirstName = "Jane" };
            var response = new CreateUserCommandResponse { Id = user.Id };

            _mapperMock.Setup(m => m.Map<User>(request)).Returns(user);
            _repoMock.Setup(r => r.AddAndSaveAsync(user)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<CreateUserCommandResponse>(user)).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(response.Id, result.Id);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenRepositoryThrows()
        {
            var request = new CreateUserCommandRequest { FirstName = "John", LastName = "Doe" };
            var user = new User { Id = Guid.NewGuid(), FirstName = "John" };
            _mapperMock.Setup(m => m.Map<User>(request)).Returns(user);
            _repoMock.Setup(r => r.AddAndSaveAsync(user)).ThrowsAsync(new Exception("DB error"));

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenMapperThrows()
        {
            var request = new CreateUserCommandRequest { FirstName = "John", LastName = "Doe" };
            _mapperMock.Setup(m => m.Map<User>(request)).Throws(new InvalidOperationException("Mapping error"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenFirstNameIsMissing()
        {
            var handler = new CreateUserCommandHandler(_repoMock.Object, _mapperMock.Object);
            var request = new CreateUserCommandRequest { FirstName = null };
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
} 