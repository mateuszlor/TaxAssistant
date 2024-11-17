using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaxAssistant.JPK.Client.Clients.Abstraction;
using TaxAssistant.JPK.Shared.Model.Abstraction;
using TaxAssistant.JPK.Shared.Model.View;

namespace TaxAssistant.JPK.Client.Clients
{
	public abstract class BaseApiClient<T> : IApiClient<T>
        where T : BaseModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
		private readonly JsonSerializerOptions _options;

        protected BaseApiClient(
            HttpClient httpClient,
            string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            
			_options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
            _options.Converters.Add(new JsonStringEnumConverter());	
		}

        public async Task DeleteAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<IList<T>?> GetAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }

            var content = await response.Content.ReadFromJsonAsync<IList<T>?>(_options);

            return content;
        }

        public async Task<T?> GetAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }

			var content = await response.Content.ReadFromJsonAsync<T?>(_options);

			return content;
		}

        public async Task<IList<Selectable<T>>?> GetSelectableAsync()
        {
            var content = await GetAsync();

            var model = content
                .Select(x => new Selectable<T>(x))
                .ToList();

            return model;
        }
    }
}
