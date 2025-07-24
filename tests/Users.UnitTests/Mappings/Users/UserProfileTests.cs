using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Users.Application.Mappings.Users;
using Users.Data.Tables;
using Users.Domain.Entities.Users.Commands.Create;
using Xunit;

namespace Users.UnitTests.Mappings.Users
{
    public class UserProfileTests
    {
        private readonly MapperConfiguration _config = new MapperConfiguration(
            cfg => cfg.AddProfile<UserProfile>(),
            // supply a no‑op logger factory in tests
            NullLoggerFactory.Instance
        );
        
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void Should_Map_CreateUserCommandRequest_To_User()
        {
            var mapper = _config.CreateMapper();
            var request = new CreateUserCommandRequest { FirstName = "John", LastName = "Doe" };
            var user = mapper.Map<User>(request);
            Assert.Equal(request.FirstName, user.FirstName);
            Assert.Equal(request.LastName, user.LastName);
        }
    }
} 