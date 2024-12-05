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
                    RequestId = "ID-1",
                    RequestDateTime = DateTime.Today.ToShortDateString(),
                    Subject = new Entity
                    {
                        Nip = "1234567890"
                    }
                }
            };
            var httpClient = new HttpClient(new MockedHttpMessageHandler(true, responseObject));
            var sut = new VatWhiteListClient(httpClient, _options);

            // Act
            var result = await sut.SearchByNip("1234567890", DateTime.Today);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().NotBeNull();
            result.Result.RequestId.Should().Be("ID-1");
            result.Result.RequestDateTime.Should().Be(DateTime.Today.ToShortDateString());
            result.Result.Subject.Should().NotBeNull();
            result.Result.Subject!.Nip.Should().Be("1234567890");
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

        [Test]
        public async Task SearchByNips_ForValidResponse_ShouldReturn200()
        {
            // Arrange
            var responseObject = new EntryListResponse
            {
                Result = new EntryList
                {
                    RequestId = "ID-1",
                    RequestDateTime = DateTime.Today.ToShortDateString(),
                    Entries = [
                        new Entry
                        {
                            Identifier = "1234567890",
                            Subjects = [
                                new Entity { Nip = "1234567890" }
                            ]
                        },
                        new EntryError
                        {
                            Identifier = "1234567890",
                            Error = new Error {
                                Code = "ERR-99",
                                Message = "Some error"
                            }
                        }
                    ]
                }
            };
            var httpClient = new HttpClient(new MockedHttpMessageHandler(true, responseObject));
            var sut = new VatWhiteListClient(httpClient, _options);

            // Act
            var result = await sut.SearchByNips(["1234567890"], DateTime.Today);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().NotBeNull();
            result.Result.RequestId.Should().Be("ID-1");
            result.Result.RequestDateTime.Should().Be(DateTime.Today.ToShortDateString());
            result.Result.Entries.Should().HaveCount(2);
            result.Result.FoundEntries.Should().HaveCount(1);
            result.Result.ErrorEntries.Should().HaveCount(1);
        }

        [Test]
        public async Task SearchByNips_ForInvalidResponse_ShouldReturn400()
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
            await sut.Awaiting(x => x.SearchByNips(["1234567890"], DateTime.Today))
                .Should()
                .ThrowAsync<ApiException>();
        }
    }
}
