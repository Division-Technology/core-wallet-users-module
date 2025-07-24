using Users.Application.Validators.Users;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Xunit;

namespace Users.UnitTests.Validators.Users
{
    public class PatchUpdateUserCommandValidatorTests
    {
        private readonly PatchUpdateUserCommandValidator _validator = new();

        [Fact]
        public void Validate_ShouldPass_ForValidRequest()
        {
            var request = new PatchUpdateUserCommand { Id = System.Guid.NewGuid(), FirstName = "John" };
            var result = _validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldFail_ForNullRequest()
        {
            var validator = new PatchUpdateUserCommandValidator();
            Assert.Throws<ArgumentNullException>(() => validator.Validate((PatchUpdateUserCommand)null!));
        }

        [Fact]
        public void Validate_ShouldFail_ForMaxLengthViolations()
        {
            var request = new PatchUpdateUserCommand { Id = System.Guid.NewGuid(), FirstName = new string('a', 101), LastName = new string('b', 101) };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_ShouldFail_ForInvalidPhoneNumber()
        {
            var request = new PatchUpdateUserCommand { Id = System.Guid.NewGuid(), PhoneNumber = "notaphone" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
        }
    }
} 