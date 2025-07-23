using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using MediatR;
using Xunit;

namespace Users.UnitTests.Behaviors
{
    public class ValidationBehaviorTests
    {
        [Fact]
        public async Task Handle_WithValidRequest_ShouldCallNext()
        {
            var validator = new DummyValidator();
            var behavior = new ValidationBehavior<DummyRequest, DummyResponse>(new[] { validator });
            var request = new DummyRequest { Name = "Test" };
            
            MediatR.RequestHandlerDelegate<DummyResponse> next = cancellationToken => Task.FromResult(new DummyResponse());

            var response = await behavior.Handle(request, next, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Handle_WithInvalidRequest_ShouldThrowValidationException()
        {
            var validator = new DummyValidator();
            var behavior = new ValidationBehavior<DummyRequest, DummyResponse>(new[] { validator });
            var request = new DummyRequest { Name = null };
            
            MediatR.RequestHandlerDelegate<DummyResponse> next = cancellationToken => Task.FromResult(new DummyResponse());

            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => behavior.Handle(request, next, CancellationToken.None));
        }

        public class DummyRequest { public string? Name { get; set; } }
        public class DummyResponse { }

        public class DummyValidator : AbstractValidator<DummyRequest>
        {
            public DummyValidator()
            {
                RuleFor(x => x.Name).NotNull();
            }
        }
    }
} 