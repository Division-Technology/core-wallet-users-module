using Users.Application.Validators.Users;
using Users.Domain.Entities.Users.Commands.Create;
using Xunit;

namespace Users.UnitTests.Validators.Users
{
    public class CreateUserCommandRequestValidatorTests
    {
        private readonly CreateUserCommandRequestValidator _validator = new();

        [Fact]
        public void Validate_ShouldPass_ForValidRequest()
        {
            var request = new CreateUserCommandRequest
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890",
                Language = "en"
            };
            var result = _validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldFail_ForInvalidPhoneNumber()
        {
            var request = new CreateUserCommandRequest { FirstName = "John", LastName = "Doe", PhoneNumber = "abc" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldFail_ForMissingFirstName()
        {
            var request = new CreateUserCommandRequest { LastName = "Doe", PhoneNumber = "+1234567890" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldFail_ForNullRequest()
        {
            var result = _validator.Validate(null as CreateUserCommandRequest);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldFail_ForMaxLengthViolations()
        {
            var request = new CreateUserCommandRequest
            {
                FirstName = new string('a', 101),
                LastName = new string('b', 101),
                PhoneNumber = "+1234567890",
                Language = "en"
            };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldFail_ForInvalidLanguageCode()
        {
            var request = new CreateUserCommandRequest
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890",
                Language = "englishlong"
            };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
        }
    }
} 