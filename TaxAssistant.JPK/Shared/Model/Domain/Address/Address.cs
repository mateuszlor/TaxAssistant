using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain.Address
{
	public class Address: IIdentifiable
	{
		public Address(string country, string postalCode, string city, string? street, string buildingNumber, string? localNumber = null)
		{
			Country = country;
			PostalCode = postalCode;
			City = city;
			Street = street;
			BuildingNumber = buildingNumber;
			LocalNumber = localNumber;
		}

		public Guid Id { get; } = Guid.NewGuid();

		public string Country { get; set; }

		public string City { get; set; }

		public string? Street { get; set; }

		public string BuildingNumber { get; set; }

		public string? LocalNumber { get; set; }

		public string PostalCode { get; set; }
	}
}