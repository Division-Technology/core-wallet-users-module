using Xunit;
using Users.Data.Migrations;

namespace Users.Data.Migrations.Tests
{
    public class InitialMigrationTests
    {
        [Fact]
        public void CanInstantiate()
        {
            // Act
            var migration = new InitialMigration();
            
            // Assert
            Assert.NotNull(migration);
        }
    }
}
