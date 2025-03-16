namespace TaxAssistant.JPK.Shared.Model.Domain.Company.Events
{
    public abstract class BaseNewCompanyWithAddressEvent : BaseNewCompanyEvent
    {
        protected BaseNewCompanyWithAddressEvent(string? address)
            : base()
        {
            Address = address;
        }

        protected BaseNewCompanyWithAddressEvent(string companyName, string taxIdentificationNumber, string? address)
            : base(companyName, taxIdentificationNumber)
        {
            Address = address;
        }

        public string? Address { get; }

        public override bool Equals(object? obj)
        {
            if (obj is BaseNewCompanyEvent e1 && !string.IsNullOrEmpty(e1.TaxIdentificationNumber))
            {
                return TaxIdentificationNumber == e1.TaxIdentificationNumber;
            }
            else if (obj is BaseNewCompanyWithAddressEvent e2)
            {
                return e2.CompanyName == CompanyName && e2.Address == Address;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TaxIdentificationNumber, CompanyName, Address);
        }
    }
}
