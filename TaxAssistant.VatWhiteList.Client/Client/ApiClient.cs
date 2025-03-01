using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.Extensions.Options;
using TaxAsistant.VatWhiteList.Client.Configuration;
using TaxAssistant.VatWhiteList.Model;

namespace TaxAsistant.VatWhiteList.Client.Client
{
    /// <summary>
    /// API client is mainly responible for making the HTTP call to the API backend.
    /// </summary>
    public abstract class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsSnapshot<VatWhiteListConfiguration> _options;
        private readonly JsonSerializerOptions _serializerOptions;

        protected ApiClient(
            HttpClient httpClient,
            IOptionsSnapshot<VatWhiteListConfiguration> options)
        {
            _httpClient = httpClient;
            _options = options;

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _serializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<T> GetAsync<T>(string url, DateTime date)
            where T : class, new()
        {
            var baseUri = new Uri(_options.Value.Url);
            var uri = new Uri(baseUri, url);
            var builder = new UriBuilder(uri);

            var query = HttpUtility.ParseQueryString(builder.Query);
            query[nameof(date)] = DateToString(date);

            builder.Query = query.ToString();

            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(builder.ToString());
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException(ex.StatusCode.GetValueOrDefault(), ex.HttpRequestError.ToString(), ex.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<T>(_serializerOptions);

                return content!;
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<Error>(_serializerOptions);

                throw new ApiException(response.StatusCode, error!.Code, error.Message);
            }
        }

        public async Task<EntityResponse> GetEntityAsync(string url, DateTime date) => await GetAsync<EntityResponse>(url, date);

        public async Task<EntityListResponse> GetEntityListAsync(string url, DateTime date) => await GetAsync<EntityListResponse>(url, date);

        public async Task<EntryListResponse> GetEntryListAsync(string url, DateTime date) => await GetAsync<EntryListResponse>(url, date);

        public async Task<EntityCheckResponse> GetEntityCheckAsync(string url, DateTime date) => await GetAsync<EntityCheckResponse>(url, date);

        protected static string DateToString(DateTime date) => date.ToString("yyyy-MM-dd");
        protected static string ListToString(IEnumerable<string> items) => string.Join(", ", items);
    }
}
