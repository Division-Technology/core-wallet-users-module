using Xunit;
using System;

namespace Users.Application.Exceptions.Tests
{
    public class BadRequestExceptionTests
    {
        [Fact]
        public void Ctor_WithMessage_SetsMessageProperty()
        {
            // Arrange
            var message = "Bad request occurred";

            // Act
            var ex = new BadRequestException(message);

            // Assert
            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public void Ctor_Parameterless_SetsDefaultMessage()
        {
            // Act
            var ex = new BadRequestException();

            // Assert
            Assert.NotNull(ex.Message); // .NET default is a non-null string
        }

        [Fact]
        public void Ctor_WithMessageAndInnerException_SetsProperties()
        {
            // Arrange
            var message = "Bad request with inner";
            var inner = new InvalidOperationException("Inner");

            // Act
            var ex = new BadRequestException(message, inner);

            // Assert
            Assert.Equal(message, ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }
}
