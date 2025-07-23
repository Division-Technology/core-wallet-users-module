using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Users.Application.Handlers.Users.Queries.ListUsers;
using Users.Domain.Entities.Users.Queries.ListUsers;
using Users.Repositories.Users;
using Users.Data.Tables;
using Users.Domain.Enums;
using Users.Application.Exceptions;
using Xunit;

namespace Users.UnitTests.Handlers.Users.Queries
{
    public class ListUsersQueryHandlerTests
    {
        private readonly Mock<IUsersRepository> _repoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly ListUsersQueryHandler _handler;

        public ListUsersQueryHandlerTests()
        {
            _handler = new ListUsersQueryHandler(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPagedUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid() },
                new User { Id = Guid.NewGuid() }
            };
            var userDtos = users.Select(u => new UserListItemDto { Id = u.Id }).ToList();
            _repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(users);
            _mapperMock.Setup(m => m.Map<UserListItemDto>(It.IsAny<User>())).Returns((User u) => new UserListItemDto { Id = u.Id });

            // Act
            var result = await _handler.Handle(new ListUsersQuery { Page = 1, PageSize = 2 }, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Users.Count);
            Assert.Equal(users[0].Id, result.Users[0].Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmpty_WhenNoUsers()
        {
            _repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<User>());
            var result = await _handler.Handle(new ListUsersQuery { Page = 1, PageSize = 2 }, CancellationToken.None);
            Assert.Empty(result.Users);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenRepositoryThrows()
        {
            _repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("DB error"));
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new ListUsersQuery { Page = 1, PageSize = 2 }, CancellationToken.None));
        }
    }
} 