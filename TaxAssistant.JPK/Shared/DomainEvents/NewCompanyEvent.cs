namespace TaxAssistant.JPK.Shared.DomainEvents
{
    public class NewCompanyEvent
	{
		public NewCompanyEvent(string companyData, string address)
		{
			CompanyData = companyData;
			Address = address;
		}

		public NewCompanyEvent(string companyName, string taxIdentificationNumber, string address)
		{
			TaxIdentificationNumber = taxIdentificationNumber;
			Address = address;

			CompanyData = $"{companyName}{Environment.NewLine}NIP: {taxIdentificationNumber}";
		}

		public string CompanyData { get; set; }
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
            return HashCode.Combine(CompanyData, TaxIdentificationNumber, Address);
        }
    }
}
