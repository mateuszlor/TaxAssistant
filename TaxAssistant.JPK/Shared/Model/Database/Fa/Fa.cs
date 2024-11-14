using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
    public class Fa : BaseModel
    {
        public virtual FaHeader Header { get; set; }

        public virtual FaControlData ControlData { get; set; }

        public virtual FaCompany Subject { get; set; }

		public virtual IList<FaInvoice> Invoices { get; set; }

		public virtual IList<FaOrder> Orders { get; set; }
	}
}
