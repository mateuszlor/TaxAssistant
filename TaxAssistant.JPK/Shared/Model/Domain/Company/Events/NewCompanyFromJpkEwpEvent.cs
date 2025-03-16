namespace TaxAssistant.JPK.Shared.Model.Domain.Company.Events
{
    public class NewCompanyFromJpkEwpEvent : BaseNewCompanyEvent
    {
        public NewCompanyFromJpkEwpEvent(string companyName, string taxIdentificationNumber, string postalCode, string city, string? street, string buildingNumber, string? localNumber, string voivodeship)
            : base(companyName, taxIdentificationNumber)
        {
            PostalCode = postalCode;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
            LocalNumber = localNumber;
            Voivodeship = voivodeship;
        }

        public string PostalCode { get; }
        public string City { get; }
        public string? Street { get; }
        public string BuildingNumber { get; }
        public string? LocalNumber { get; }
        public string Voivodeship { get; }

        public override bool Equals(object? obj)
        {
            if (obj is NewCompanyFromJpkEwpEvent e)
            {
                return TaxIdentificationNumber == e.TaxIdentificationNumber;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TaxIdentificationNumber, CompanyName, PostalCode, City, Street, BuildingNumber, LocalNumber, Voivodeship);
        }
    }
}
