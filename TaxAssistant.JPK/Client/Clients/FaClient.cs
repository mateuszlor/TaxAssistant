using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Database.Fa;

namespace TaxAssistant.JPK.Client.Clients
{
    public class FaClient : BaseApiClient<Fa>
    {
        public FaClient(HttpClient httpClient) : base(httpClient, "Fa")
        {
        }
    }
}
