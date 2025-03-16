using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.Client.Clients
{
    public class InvoiceClient : BaseApiClient<Invoice>
    {
        public InvoiceClient(HttpClient httpClient) : base(httpClient, "Invoice")
        {
        }
    }
}
