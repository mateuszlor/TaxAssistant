namespace TaxAssistant.JPK.Shared.Model.Database.Kpir
{
    public class KpirCompany : BaseModel
    {
        public Guid KpirId { get; set; }

        public virtual Kpir Kpir { get; set; }

        public string TaxIdentificationNumber { get; set; }

        public string NationalStatisticNumber { get; set; }
        
        public string Name { get; set; }

        public Guid AddressId { get; set; }

        public virtual KpirCompanyAddress Address { get; set; }
    }
}