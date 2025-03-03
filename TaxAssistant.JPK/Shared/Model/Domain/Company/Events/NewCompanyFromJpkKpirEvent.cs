namespace TaxAssistant.JPK.Shared.Model.Domain.Company.Events
{
    public class NewCompanyFromJpkKpirEvent : BaseNewCompanyWithAddressEvent
    {
        public NewCompanyFromJpkKpirEvent(string? companyData, string? address)
            : base(address)
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
        }
    }
}
