namespace TaxAssistant.JPK.Shared.Model.Database.Ewp
{
    public class EwpCompanyAddress : BaseModel
    {
        public virtual EwpCompany Company { get; set; }

        public string Voivodeship { get; set; }

        public string City { get; set; }

        public string? Street { get; set; }

        public string BuildingNumber { get; set; }

        public string? LocalNumber { get; set; }

        public string PostalCode { get; set; }
    }
}