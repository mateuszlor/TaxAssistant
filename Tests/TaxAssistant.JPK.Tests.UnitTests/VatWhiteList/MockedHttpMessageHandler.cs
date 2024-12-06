using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using TaxAsistant.VatWhiteList.Client;
using System.Net.Http.Headers;

namespace TaxAssistant.JPK.Tests.UnitTests.VatWhiteList
{
    internal class MockedHttpMessageHandler : HttpMessageHandler
    {
        private readonly bool _shouldSucceed;
        private readonly object _responseContent;
        private readonly JsonSerializerOptions _serializerOptions;

        public MockedHttpMessageHandler(bool shouldSucceed, object responseContent)
        {
            _shouldSucceed = shouldSucceed;
            _responseContent = responseContent;

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            _serializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage
            {
                StatusCode = _shouldSucceed
                    ? HttpStatusCode.OK
                    : HttpStatusCode.BadRequest,
                Content = JsonContent.Create(_responseContent, _responseContent.GetType(), new MediaTypeHeaderValue("application/json"), _serializerOptions)
            };

            return await Task.FromResult(result);
        }
    }
}
