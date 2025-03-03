using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain.Company.Events
{
    public class NewCompanyEvent : IDomainEvent
	{
		public NewCompanyEvent(string? companyData, string? address)
		{
			if (string.IsNullOrWhiteSpace(companyData))
			{
				throw new ArgumentNullException(nameof(companyData));
			}

			var companyDataParts = companyData!.Split("\n");

			CompanyName = companyDataParts[0].Trim();

			if (companyDataParts.Length > 1)
			{
				TaxIdentificationNumber = companyDataParts.Last();

                if (TaxIdentificationNumber.StartsWith("NIP:"))
                {
                    TaxIdentificationNumber = TaxIdentificationNumber.Substring(4);
                }
                else if (TaxIdentificationNumber.StartsWith("PESEL:"))
                {
                    TaxIdentificationNumber = TaxIdentificationNumber.Substring(6);
                }

                TaxIdentificationNumber = TaxIdentificationNumber.Trim();
			}

			Address = address;
		}

		public NewCompanyEvent(string companyName, string taxIdentificationNumber, string address)
		{
			CompanyName = companyName;
			TaxIdentificationNumber = taxIdentificationNumber;
			Address = address;
		}

        public NewCompanyEvent(string companyName, string taxIdentificationNumber, string postalCode, string city, string? street, string buildingNumber, string? localNumber, string voivodeship)
        {
            CompanyName = companyName;
            TaxIdentificationNumber = taxIdentificationNumber;

            PostalCode = postalCode;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
            LocalNumber = localNumber;
            Voivodeship = voivodeship;

            DetailedAddress = true;
        }

        public Origin Origin { get; } = Origin.JPK;
        public string CompanyName { get; }
		public string? TaxIdentificationNumber { get; }
		public string? Address { get; }
        public string? PostalCode { get; }
        public string? City { get; }
        public string? Street { get; }
        public string? BuildingNumber { get; }
        public string? LocalNumber { get; }
        public string? Voivodeship { get; }
        public bool DetailedAddress { get; }

        public override bool Equals(object? obj)
        {
			if (obj is NewCompanyEvent e)
			{
				if (!string.IsNullOrEmpty(TaxIdentificationNumber) && !string.IsNullOrEmpty(e.TaxIdentificationNumber))
				{
					return TaxIdentificationNumber == e.TaxIdentificationNumber;
				}
				else if (string.IsNullOrEmpty(TaxIdentificationNumber) && string.IsNullOrEmpty(e.TaxIdentificationNumber))
				{
					return e.CompanyName == CompanyName && e.Address == Address;
				}
			}

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TaxIdentificationNumber, CompanyName, Address, HashCode.Combine(PostalCode, City, Street, BuildingNumber, LocalNumber, Voivodeship));
        }
    }
}
