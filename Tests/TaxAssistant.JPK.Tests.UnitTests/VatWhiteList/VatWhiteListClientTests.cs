using System.Net.Http;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using TaxAsistant.VatWhiteList.Client.Client;
using TaxAsistant.VatWhiteList.Client.Configuration;
using TaxAsistant.VatWhiteList.Client.Model;

namespace TaxAssistant.JPK.Tests.UnitTests.VatWhiteList
{
    public class VatWhiteListClientTests
    {
        private IOptionsSnapshot<VatWhiteListConfiguration> _options;

        [SetUp]
        public void Setup()
        {
            
            _options = Substitute.For<IOptionsSnapshot<VatWhiteListConfiguration>>();
            _options.Value.Returns(new VatWhiteListConfiguration { Url = "http://example.org" });

        }

        [Test]
        public async Task SearchByNip_ForValidResponse_ShouldReturn200()
        {
            // Arrange
            var responseObject = new EntityResponse
            {
                Result = new EntityItem
                {
                    Subject = new Entity()
                }
            };
            var httpClient = new HttpClient(new MockedHttpMessageHandler(true, responseObject));
            var sut = new VatWhiteListClient(httpClient, _options);

            // Act
            var result = await sut.SearchByNip("1234567890", DateTime.Today);

            // Assert
            result.Should().NotBeNull();
        }

        [Test]
        public async Task SearchByNip_ForInvalidResponse_ShouldReturn400()
        {
            // Arrange
            var responseObject = new EntityResponse
            {
                Result = new EntityItem
                {
                    Subject = null
                }
            };
            var httpClient = new HttpClient(new MockedHttpMessageHandler(false, responseObject));
            var sut = new VatWhiteListClient(httpClient, _options);

            // Act && Assert
            await sut.Awaiting(x => x.SearchByNip("1234567890", DateTime.Today))
                .Should()
                .ThrowAsync<ApiException>();
        }
    }
}
