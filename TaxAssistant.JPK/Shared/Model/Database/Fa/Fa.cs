using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
	public class Fa : BaseModel, IAggregate
	{
		public IList<IDomainEvent> Events { get; } = [];

		public virtual FaHeader Header { get; set; }

		public virtual FaControlData ControlData { get; set; }

		public virtual FaCompany Subject { get; set; }

		public virtual IList<FaInvoice> Invoices { get; set; }

		public virtual IList<FaOrder> Orders { get; set; }
	}
}
