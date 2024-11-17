using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.Client.Clients
{
	public class CompanyClient : BaseApiClient<Company>
	{
		public CompanyClient(HttpClient httpClient) : base(httpClient, "Company")
		{
		}
	}
}
