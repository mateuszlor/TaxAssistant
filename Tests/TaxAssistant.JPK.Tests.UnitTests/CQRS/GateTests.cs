using FluentAssertions;
using NSubstitute;
using TaxAssistant.CQRS;
using TaxAssistant.CQRS.Abstraction;
using TaxAssistant.JPK.Tests.CQRS.Model;

namespace TaxAssistant.JPK.Tests.CQRS
{
	public class GateTests
    {
        private IServiceProvider _serviceProvider;
        private Gate _sut;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
            _sut = new Gate(_serviceProvider);
        }

        [Test]
        public async Task HandleAsync_ForNullCommand_ShouldThrow()
        {
            // Act && Assert
            await _sut.Awaiting(x => x.HandleAsync<ExampleCommand>(null))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Handler for TaxAssistant.JPK.Tests.CQRS.Model.ExampleCommand not found");
        }

        [Test]
        public async Task HandleAsync_ForNoHandler_ShouldThrow()
        {
            // Arrange
            var command = new ExampleCommand();

            // Act && Assert
            await _sut.Awaiting(x => x.HandleAsync(command))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Handler for TaxAssistant.JPK.Tests.CQRS.Model.ExampleCommand not found");
        }

        [Test]
        public async Task HandleAsync_ForFoundHandler_ShouldSucceed()
        {
            // Arrange
            var command = new ExampleCommand();
            _serviceProvider.GetService(typeof(ICommandHandler<ExampleCommand>)).Returns(new ExampleCommandHandler());

            // Act 
            await _sut.HandleAsync(command);
            Assert.Pass();
        }

        [Test]
        public async Task HandleAsyncWithResult_ForNullCommand_ShouldThrow()
        {
            // Act && Assert
            await _sut.Awaiting(x => x.HandleAsync<ExampleCommand, ExampleCommandResult>(null))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Handler for TaxAssistant.JPK.Tests.CQRS.Model.ExampleCommand, TaxAssistant.JPK.Tests.CQRS.Model.ExampleCommandResult not found");
        }

        [Test]
        public async Task HandleAsyncWithResult_ForNoHandler_ShouldThrow()
        {
            // Arrange
            var command = new ExampleCommand();

            // Act && Assert
            await _sut.Awaiting(x => x.HandleAsync<ExampleCommand, ExampleCommandResult>(command))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Handler for TaxAssistant.JPK.Tests.CQRS.Model.ExampleCommand, TaxAssistant.JPK.Tests.CQRS.Model.ExampleCommandResult not found");
        }

        [Test]
        public async Task HandleAsyncWithResult_ForFoundHandler_ShouldSucceed()
        {
            // Arrange
            var command = new ExampleCommand();
            _serviceProvider.GetService(typeof(ICommandHandler<ExampleCommand, ExampleCommandResult>)).Returns(new ExampleCommandHandlerWithResult());

            // Act 
            var result = await _sut.HandleAsync<ExampleCommand, ExampleCommandResult>(command);

            // Assert
            result.Should().NotBeNull();
        }
    }
}