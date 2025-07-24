using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Data.Tables;
using Users.Data.Configurations.Users;

namespace Users.Data.Configurations.Users.Tests
{
    public class UsersConfigurationTests
    {
        [Fact]
        public void Configure_Should_NotThrow_WithValidBuilder()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var builder = modelBuilder.Entity<User>();
            var config = new UsersConfiguration();

            // Act & Assert
            var ex = Record.Exception(() => config.Configure(builder));
            Assert.Null(ex);
        }
    }
}
