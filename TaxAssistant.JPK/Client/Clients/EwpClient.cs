using TaxAssistant.JPK.Shared.Model.Database.Ewp;

namespace TaxAssistant.JPK.Client.Clients
{
    public class EwpClient : BaseApiClient<Ewp>
    {
        public EwpClient(HttpClient httpClient) : base(httpClient, "Ewp")
        {
        }
    }
}
