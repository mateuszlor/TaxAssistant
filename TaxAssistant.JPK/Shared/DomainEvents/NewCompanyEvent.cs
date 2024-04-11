namespace TaxAssistant.JPK.Shared.DomainEvents
{
    public class NewCompanyEvent
    {
        public NewCompanyEvent(string companyData, string address)
        {
            CompanyData = companyData;
            Address = address;
        }

        public string CompanyData { get; set; }

        public string Address { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is NewCompanyEvent e)
            {
                return e.CompanyData == CompanyData && e.Address == Address;
            }

            return false;
        }
    }
}
