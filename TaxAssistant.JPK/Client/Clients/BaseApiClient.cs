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

            return await HandleResponse<IList<T>>(response);
        }

        public async Task<T?> GetAsync(Guid id, string? action = null)
        {
            var uri = $"{_baseUrl}/{id}";

            if (!string.IsNullOrEmpty(action))
            {
                uri += $"/{action}";
            }

            var response = await _httpClient.GetAsync(uri);

            return await HandleResponse<T>(response);
        }

        public async Task<IList<Selectable<T>>?> GetSelectableAsync()
        {
            var content = await GetAsync();

            var model = content
                .Select(x => new Selectable<T>(x))
                .ToList();

            return model;
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            var response = await _httpClient.PostAsJsonAsync<T>($"{_baseUrl}/{entity.Id}", entity, _options);

            return await HandleResponse<T>(response);
        }

        private async Task<TResponse?> HandleResponse<TResponse>(HttpResponseMessage response)
            where TResponse: class
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<TResponse?>(_options);

            return content;
        }
    }
}
