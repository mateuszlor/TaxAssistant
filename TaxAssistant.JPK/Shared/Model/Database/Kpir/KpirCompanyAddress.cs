namespace TaxAssistant.JPK.Shared.Model.Database.Kpir
{
    public class KpirCompanyAddress : BaseModel
    {
        public virtual KpirCompany Company { get; set; }

        public string CountryCode { get; set; }

        public string Voivodeship { get; set; }

        public string City { get; set; }

        public string? Street { get; set; }

        public string BuildingNumber { get; set; }

        public string? LocalNumber { get; set; }

        public string PostalCode { get; set; }

        public string PostOffice { get; set; }
    }
}