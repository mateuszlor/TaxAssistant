using Newtonsoft.Json;
using TaxAssistant.JPK.Client.Clients.Abstraction;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.View;

namespace TaxAssistant.JPK.Client.Clients
{
    public abstract class BaseApiClient<T> : IApiClient<T>
        where T : BaseModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        protected BaseApiClient(
            HttpClient httpClient,
            string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"Kpir/{id}");
        }

        public async Task<IList<T>> GetAsync()
        {
            var response = await _httpClient.GetAsync("Kpir");

            var stringContent = await response.Content.ReadAsStringAsync();

            var content = JsonConvert.DeserializeObject<IList<T>>(stringContent);

            return content;
        }

        public async Task<T> GetAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"Kpir/{id}");

            var stringContent = await response.Content.ReadAsStringAsync();

            var content = JsonConvert.DeserializeObject<T>(stringContent);

            return content;
        }

        public async Task<IList<Selectable<T>>> GetSelectableAsync()
        {
            var content = await GetAsync();

            var model = content
                .Select(x => new Selectable<T>(x))
                .ToList();

            return model;
        }
    }
}
