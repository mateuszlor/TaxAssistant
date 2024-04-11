using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Client.Clients
{
    public class KpirClient : BaseApiClient<Kpir>
    {
        public KpirClient(HttpClient httpClient) : base(httpClient, "kpir")
        {
        }
    }
}
