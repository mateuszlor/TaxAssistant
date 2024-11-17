namespace TaxAssistant.JPK.Shared.Model.Domain.Company
{
	public class Company : BaseDomainModel
	{
		public Company(Origin origin, string taxIdentificationNumber, string name, Address.Address address, string? nationalStatisticNumber = null)
			: base(origin)
		{
			TaxIdentificationNumber = taxIdentificationNumber;
			Name = name;
			Address = address;
			NationalStatisticNumber = nationalStatisticNumber;
		}

		public string TaxIdentificationNumber { get; internal set; }

		public string Name { get; internal set; }

		public string? NationalStatisticNumber { get; internal set; }

		public virtual Address.Address Address { get; internal set; }
	}
}
