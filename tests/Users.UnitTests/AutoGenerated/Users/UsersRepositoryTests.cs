using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Users.Data;
using Users.Data.Tables;

namespace Users.Repositories.Users.Tests
{
    public class UsersRepositoryTests
    {
        private UsersDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new UsersDbContext(options);
        }

        [Fact]
        public void Ctor_Should_NotThrow_WithValidDbContext()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();

            // Act & Assert
            var repo = new UsersRepository(dbContext);
            Assert.NotNull(repo);
        }

        [Fact]
        public async Task GetAsync_Should_ReturnUser_WhenPredicateMatches()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var user = new User { Id = Guid.NewGuid(), FirstName = "Test", LastName = "User" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            var repo = new UsersRepository(dbContext);

            // Act
            var result = await repo.GetAsync(u => u.Id == user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task GetAsync_Should_ReturnNull_WhenNoMatch()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var repo = new UsersRepository(dbContext);

            // Act
            var result = await repo.GetAsync(u => u.Id == Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnUser_WhenExists()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var user = new User { Id = Guid.NewGuid(), FirstName = "Test", LastName = "User" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            var repo = new UsersRepository(dbContext);

            // Act
            var result = await repo.GetByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnNull_WhenNotExists()
        {
            // Arrange
            var dbContext = CreateInMemoryDbContext();
            var repo = new UsersRepository(dbContext);

            // Act
            var result = await repo.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}
