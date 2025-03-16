namespace TaxAssistant.JPK.Shared.Model.Domain.Company.Events
{
    public class NewCompanyFromJpkFaEvent : BaseNewCompanyWithAddressEvent
    {
        public NewCompanyFromJpkFaEvent(string companyName, string taxIdentificationNumber, string address)
            : base(companyName, taxIdentificationNumber, address)
        {
        }

        public override bool Equals(object? obj)
        {
            if (obj is BaseNewCompanyEvent e)
            {
                return TaxIdentificationNumber == e.TaxIdentificationNumber;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TaxIdentificationNumber, CompanyName, Address);
        }
    }
}
