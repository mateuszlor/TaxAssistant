using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
	public class FaInvoiceCompany : BaseModel
	{
		public string? TaxIdentificationNumber { get; set; }

		public string? Name { get; set; }

		public string? Address { get; set; }
	}
}