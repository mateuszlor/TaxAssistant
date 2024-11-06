using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TaxAssistant.CQRS;
using TaxAssistant.JPK.Server.Controllers;
using TaxAssistant.JPK.Shared.Commands;

namespace TaxAssistant.JPK.Tests.Controller
{
    public class AggregateControllerTests
    {
        private IGate _gate;
        private AggregateController _sut;

        [SetUp]
        public void Setup()
        {
            _gate = Substitute.For<IGate>();
            _sut = new AggregateController(_gate);
        }

        [Test]
        public async Task Aggregate_ShouldReturn200()
        {
            // Act
            var result = await _sut.Aggregate(new AggregateKpirCommand());

            // Assert
            await _gate.Received(1).HandleAsync<AggregateKpirCommand, AggregateKpirCommandResult>(Arg.Any<AggregateKpirCommand>());
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task Aggregate_ForError_ShouldReturn400()
        {
            // Arrange
            _gate.HandleAsync<AggregateKpirCommand, AggregateKpirCommandResult>(Arg.Any<AggregateKpirCommand>()).Throws(new Exception("some error"));

            // Act
            var result = await _sut.Aggregate(new AggregateKpirCommand());

            // Assert
            await _gate.Received(1).HandleAsync<AggregateKpirCommand, AggregateKpirCommandResult>(Arg.Any<AggregateKpirCommand>());
            result.Should().BeOfType<ObjectResult>();

            var resultObject = result as ObjectResult;
            resultObject.Should().NotBeNull();
            resultObject.StatusCode.Should().Be(500);
            resultObject.Value.Should().BeOfType<string>();

            var error = resultObject.Value as string;
            error.Should().Be("some error");
        }
    }
}
