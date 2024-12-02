using System.Net;
using System.Net.Http.Json;

namespace TaxAssistant.JPK.Tests.UnitTests.VatWhiteList
{
    internal class MockedHttpMessageHandler : HttpMessageHandler
    {
        private readonly bool _shouldSucceed;
        private readonly object _responseContent;

        public MockedHttpMessageHandler(bool shouldSucceed, object responseContent)
        {
            _shouldSucceed = shouldSucceed;
            _responseContent = responseContent;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage
            {
                StatusCode = _shouldSucceed
                    ? HttpStatusCode.OK
                    : HttpStatusCode.BadRequest,
                Content = JsonContent.Create(_responseContent)

            };

            return await Task.FromResult(result);
        }
    }
}
