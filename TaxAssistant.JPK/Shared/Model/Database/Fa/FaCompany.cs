using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
    public class FaCompany : BaseModel
	{
		public Guid FaId { get; set; }

		public virtual Fa Fa { get; set; }

		public string Name { get; set; }

		public Guid AddressId { get; set; }

		public virtual FaCompanyAddress? Address { get; set; }
        public string TaxIdentificationNumber { get; internal set; }
        public TaxIdentificationNumberType TaxIdentificationNumberType { get; internal set; }
    }
}