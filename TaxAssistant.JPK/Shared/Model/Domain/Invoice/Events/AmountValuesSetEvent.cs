using TaxAssistant.DDD;

namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
    public class AmountValuesSetEvent : BaseDomainEvent
	{
		public AmountValuesSetEvent(Guid id, bool vatDataSpecified) : base(id)
        {
            VatDataSpecified = vatDataSpecified;
        }

        public bool VatDataSpecified { get; }
    }
}