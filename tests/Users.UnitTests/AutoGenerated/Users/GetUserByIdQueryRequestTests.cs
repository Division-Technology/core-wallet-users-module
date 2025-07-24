using Xunit;
using System;
using Users.Domain.Entities.Users.Queries.GetById;

namespace Users.Domain.Entities.Users.Queries.GetById.Tests
{
    public class GetUserByIdQueryRequestTests
    {
        [Fact]
        public void Ctor_WithId_SetsIdProperty()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var request = new GetUserByIdQueryRequest(id);

            // Assert
            Assert.Equal(id, request.Id);
        }

        [Fact]
        public void Properties_Should_Set_And_Get_Correctly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new GetUserByIdQueryRequest(id)
            {
                Id = Guid.NewGuid()
            };

            // Assert
            Assert.NotEqual(id, request.Id);
        }
    }
}
