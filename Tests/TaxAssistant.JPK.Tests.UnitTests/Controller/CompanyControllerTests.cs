using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TaxAssistant.CQRS.Abstraction;
using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public class CompanyControllerTests : BaseControllerTests<Company>
    {
        private readonly IGate _gate;

        private CompanyController? _typedSut => _sut as CompanyController;

        public CompanyControllerTests() : base(new Company(Origin.JPK, "1234567890", "Monsters Inc."))
        {
            _gate = Substitute.For<IGate>();
            _getSut = () => new CompanyController(_repository, _gate);
        }

        [Test]
        public async Task SynchronizeWithVatWhiteList_ForHandlerSuccess_ShouldReturn200()
        {
            // Arrange
            var guid = Guid.NewGuid();

            _gate
                .HandleAsync<SynchronizeWithVatWhiteListCommand, SynchronizeWithVatWhiteListCommandResult>(Arg.Any<SynchronizeWithVatWhiteListCommand>())
                .Returns(Task.FromResult(new SynchronizeWithVatWhiteListCommandResult { Company = _mockedResult }));

            // Act
            var result = await _typedSut!.SynchronizeWithVatWhiteList(guid);

            // Assert
            await _gate.Received(1).HandleAsync<SynchronizeWithVatWhiteListCommand, SynchronizeWithVatWhiteListCommandResult>(Arg.Is<SynchronizeWithVatWhiteListCommand>(x => x.CompanyId == guid));

            result.Should().BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().BeAssignableTo<SynchronizeWithVatWhiteListCommandResult>();

            var objectResultValue = objectResult.Value as SynchronizeWithVatWhiteListCommandResult;
            objectResultValue.Should().NotBeNull();
        }

        [Test]
        public async Task SynchronizeWithVatWhiteList_ForHandlerError_ShouldReturn400()
        {
            // Arrange
            var guid = Guid.NewGuid();

            _gate
                .HandleAsync<SynchronizeWithVatWhiteListCommand, SynchronizeWithVatWhiteListCommandResult>(Arg.Any<SynchronizeWithVatWhiteListCommand>())
                .Returns(Task.FromResult(new SynchronizeWithVatWhiteListCommandResult { Error = "Some known error" }));

            // Act
            var result = await _typedSut!.SynchronizeWithVatWhiteList(guid);

            // Assert
            await _gate.Received(1).HandleAsync<SynchronizeWithVatWhiteListCommand, SynchronizeWithVatWhiteListCommandResult>(Arg.Is<SynchronizeWithVatWhiteListCommand>(x => x.CompanyId == guid));

            result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.Value.Should().BeOfType<string>();
            badRequestResult.Value.Should().Be("Some known error");
        }

        [Test]
        public async Task SynchronizeWithVatWhiteList_ForException_ShouldReturn500()
        {
            // Arrange
            var guid = Guid.NewGuid();

            _gate
                .HandleAsync<SynchronizeWithVatWhiteListCommand, SynchronizeWithVatWhiteListCommandResult>(Arg.Any<SynchronizeWithVatWhiteListCommand>())
                .Throws(new Exception("some error"));

            // Act
            var result = await _typedSut!.SynchronizeWithVatWhiteList(guid);

            // Assert
            await _gate.Received(1).HandleAsync<SynchronizeWithVatWhiteListCommand, SynchronizeWithVatWhiteListCommandResult>(Arg.Is<SynchronizeWithVatWhiteListCommand>(x => x.CompanyId == guid));

            result.Should().BeOfType<ObjectResult>();

            var objectResult = result as ObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().BeOfType<string>();
            objectResult.Value.Should().Be("some error");
        }
    }
}
