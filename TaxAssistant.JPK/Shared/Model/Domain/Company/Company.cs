using System.Text.Json.Serialization;

namespace TaxAssistant.JPK.Shared.Model.Domain.Company
{
	[JsonSerializable(typeof(Company))]
	public class Company : BaseDomainModel
	{
		[JsonConstructor]
		public Company(Origin origin, string? taxIdentificationNumber, string name, string? nationalStatisticNumber = null)
			: base(origin)
		{
			TaxIdentificationNumber = taxIdentificationNumber;
			Name = name;
			NationalStatisticNumber = nationalStatisticNumber;
		}

		public Company(Origin origin, string? taxIdentificationNumber, string name, Address.Address address, string? nationalStatisticNumber = null)
			: this(origin, taxIdentificationNumber, name, nationalStatisticNumber)
		{
			Address = address;
			AddressId = address?.Id;
		}

		public Company(Origin origin, string? taxIdentificationNumber, string name, Guid addressId, string? nationalStatisticNumber = null)
			: this(origin, taxIdentificationNumber, name, nationalStatisticNumber)
		{
			AddressId = addressId;
		}

		public string? TaxIdentificationNumber { get; set; }

		public string Name { get; set; }

		public string? NationalStatisticNumber { get; set; }

		public Guid? AddressId { get; internal set; }

		[JsonIgnore]
		public virtual Address.Address? Address { get; internal set; }
	}
}
