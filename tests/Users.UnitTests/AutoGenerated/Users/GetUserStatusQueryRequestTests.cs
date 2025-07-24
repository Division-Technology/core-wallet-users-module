using Xunit;
using System;
using Users.Domain.Entities.Users.Queries.GetStatus;

namespace Users.Domain.Entities.Users.Queries.GetStatus.Tests
{
    public class GetUserStatusQueryRequestTests
    {
        [Fact]
        public void Ctor_WithId_SetsIdProperty()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var request = new GetUserStatusQueryRequest(id);

            // Assert
            Assert.Equal(id, request.Id);
        }

        [Fact]
        public void Properties_Should_Set_And_Get_Correctly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new GetUserStatusQueryRequest(id)
            {
                Id = Guid.NewGuid()
            };

            // Assert
            Assert.NotEqual(id, request.Id);
        }
    }
}
