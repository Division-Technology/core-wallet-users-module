using Xunit;
using System;
using Users.Domain.Entities.Users.Queries.GetByPhone;

namespace Users.Domain.Entities.Users.Queries.GetByPhone.Tests
{
    public class GetUserByPhoneQueryResponseTests
    {
        [Fact]
        public void Properties_Should_Set_And_Get_Correctly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var response = new GetUserByPhoneQueryResponse { Id = id };

            // Assert
            Assert.Equal(id, response.Id);
        }
    }
}
