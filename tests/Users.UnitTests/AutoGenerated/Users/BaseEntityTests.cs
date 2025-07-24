using Xunit;
using System;

namespace Users.Data.Tables.Tests
{
    public class BaseEntityTests
    {
        private class TestEntity : BaseEntity { }

        [Fact]
        public void Properties_Should_Set_And_Get_Correctly()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var entity = new TestEntity
            {
                CreatedAt = now,
                ModifiedAt = now.AddMinutes(1),
                IsDeleted = true
            };

            // Assert
            Assert.Equal(now, entity.CreatedAt);
            Assert.Equal(now.AddMinutes(1), entity.ModifiedAt);
            Assert.True(entity.IsDeleted);
        }
    }
}
