using Xunit;
using Microsoft.EntityFrameworkCore;
using Users.Data;
using Users.Data.Tables;

namespace Users.Data.Tests
{
    public class UsersDbContextTests
    {
        [Fact]
        public void Ctor_WithOptions_Should_Initialize_UsersDbSet()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            // Act
            var context = new UsersDbContext(options);

            // Assert
            Assert.NotNull(context.Users);
            Assert.IsAssignableFrom<DbSet<User>>(context.Users);
        }
    }
}
