namespace TaxAssistant.JPK.Shared.Model.Database.Ewp
{
    public class EwpCompany : BaseModel
    {
        public Guid EwpId { get; set; }

        public virtual Ewp Ewp { get; set; }

        public string TaxIdentificationNumber { get; set; }

        public string Name { get; set; }

        public Guid AddressId { get; set; }

        public virtual EwpCompanyAddress Address { get; set; }
    }
}