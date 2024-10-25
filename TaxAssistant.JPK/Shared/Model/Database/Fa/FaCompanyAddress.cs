namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
	public class FaCompanyAddress : BaseModel
	{
		public virtual FaCompany Company { get; set; }

		public string CountryCode { get; set; }

		public string Voivodeship { get; set; }

		public string City { get; set; }

		public string? Street { get; set; }

		public string BuildingNumber { get; set; }

		public string? LocalNumber { get; set; }

		public string PostalCode { get; set; }

		public string Municipality { get; internal set; }

		public string District { get; internal set; }
	}
}