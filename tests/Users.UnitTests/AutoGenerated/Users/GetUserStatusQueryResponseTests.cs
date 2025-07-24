using Xunit;
using System;
using Users.Domain.Entities.Users.Queries.GetStatus;
using Users.Domain.Enums;

namespace Users.Domain.Entities.Users.Queries.GetStatus.Tests
{
    public class GetUserStatusQueryResponseTests
    {
        [Fact]
        public void Ctor_WithId_SetsIdProperty()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var response = new GetUserStatusQueryResponse(id);

            // Assert
            Assert.Equal(id, response.Id);
        }

        [Fact]
        public void Properties_Should_Set_And_Get_Correctly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var status = RegistrationStatus.Registered;
            var response = new GetUserStatusQueryResponse(id)
            {
                Status = status
            };

            // Assert
            Assert.Equal(id, response.Id);
            Assert.Equal(status, response.Status);
        }
    }
}
