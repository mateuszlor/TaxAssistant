using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TaxAsistant.VatWhiteList.Client.Client;
using TaxAssistant.VatWhiteList.Model;
using TaxAssistant.JPK.Server.Controllers.Action;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public class VatWhiteListControllerTests
    {
        private readonly IVatWhiteListClient _client;
        private readonly VatWhiteListController _sut;

        public VatWhiteListControllerTests()
        {
            _client = Substitute.For<IVatWhiteListClient>();
            _sut = new VatWhiteListController(_client);
        }

        [Test]
        public async Task SearchByNip_ForValidSourceResponse_ReturnsResult()
        {
            // Arrange
            var responseObject = new EntityResponse
            {
                Result = new EntityItem { 
                    Subject = new Entity
                    {
                        Nip = "1234567890",
                        Name = "Monsters Inc."
                    }
                }
            };

            _client.SearchByNip("1234567890", Arg.Any<DateTime>()).Returns(Task.FromResult(responseObject));

            // Act
            var result = await _sut.SearchByNip("1234567890");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();
            objectResult!.Value.Should().BeAssignableTo<EntityResponse>();

            var resultObject = objectResult.Value as EntityResponse;
            resultObject.Should().NotBeNull();
            resultObject!.Result.Should().NotBeNull();
            resultObject.Result.Subject.Should().NotBeNull();
            resultObject.Result.Subject!.Nip.Should().Be("1234567890");
            resultObject.Result.Subject.Name.Should().Be("Monsters Inc.");
        }

        [Test]
        public async Task SearchByNip_ForException_ReturnsError()
        {
            // Arrange
            _client.SearchByNip("1234567890", Arg.Any<DateTime>()).Throws(new ApiException(HttpStatusCode.BadRequest, "ERR-99", "Some error"));

            // Act
            var result = await _sut.SearchByNip("1234567890");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();

            var badRequestResult = result as ObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult!.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeOfType<Error>();

            var error = badRequestResult.Value as Error;
            error.Should().NotBeNull();
            error!.Code.Should().Be("ERR-99");
            error.Message.Should().Be("Some error");
        }
    }
}
