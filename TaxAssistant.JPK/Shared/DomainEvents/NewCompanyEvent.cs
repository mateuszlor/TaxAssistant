using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Shared.DomainEvents
{
    public class NewCompanyEvent : IDomainEvent
	{
		public NewCompanyEvent(string companyData, string address)
		{
			CompanyName = companyData.Split("\n").FirstOrDefault();
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
		public string Address { get; set; }

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
