using TaxAssistant.JPK.Shared.Model.Database;

namespace TaxAssistant.JPK.Client.Clients
{
    public class ImportClient : BaseApiClient<Import>
    {
        public ImportClient(HttpClient httpClient) : base(httpClient, "import")
        {
        }
    }
}
