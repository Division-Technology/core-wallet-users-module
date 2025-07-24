using Xunit;
using System;
using Users.Domain.Entities.Users.Queries.GetById;

namespace Users.Domain.Entities.Users.Queries.GetById.Tests
{
    public class GetUserByIdQueryResponseTests
    {
        [Fact]
        public void Properties_Should_Set_And_Get_Correctly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var response = new GetUserByIdQueryResponse { Id = id };

            // Assert
            Assert.Equal(id, response.Id);
        }
    }
}
