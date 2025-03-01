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
			CompanyData = companyData;
		}

		public NewCompanyEvent(string companyName, string taxIdentificationNumber, string address)
		{
			CompanyName = companyName;
			TaxIdentificationNumber = taxIdentificationNumber;
			Address = address;

			CompanyData = $"{companyName}{Environment.NewLine}NIP: {taxIdentificationNumber}";
		}

		public string CompanyData { get; set; }
		public string CompanyName { get; }
		public string? TaxIdentificationNumber { get; }
		public string? Address { get; set; }

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
					return e.CompanyData == CompanyData && e.Address == Address;
				}
			}

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TaxIdentificationNumber, CompanyName, CompanyData, Address);
        }
    }
}
