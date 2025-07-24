using Xunit;
using Users.Domain.Entities.Users.Queries.GetByPhone;

namespace Users.Domain.Entities.Users.Queries.GetByPhone.Tests
{
    public class GetUserByPhoneQueryTests
    {
        [Fact]
        public void Ctor_WithPhoneNumber_SetsPhoneNumberProperty()
        {
            // Arrange
            var phoneNumber = "+1234567890";

            // Act
            var query = new GetUserByPhoneQuery(phoneNumber);

            // Assert
            Assert.Equal(phoneNumber, query.PhoneNumber);
        }

        [Fact]
        public void Properties_Should_Set_And_Get_Correctly()
        {
            // Arrange
            var phoneNumber = "+1234567890";
            var query = new GetUserByPhoneQuery(phoneNumber)
            {
                PhoneNumber = "+0987654321"
            };

            // Assert
            Assert.Equal("+0987654321", query.PhoneNumber);
        }
    }
}
