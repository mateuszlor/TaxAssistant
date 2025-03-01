using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain.Events
{
    public class NewFixedAssetEvent : IDomainEvent
    {
        public NewFixedAssetEvent(string categoryCode, string description, string documentNumber, DateTime transferDate, DateTime acceptanceDate, decimal? initialValue, decimal? updatedInitialValue)
        {
            CategoryCode = categoryCode;
            DocumentNumber = documentNumber;
            Description = description;
            TransferDate = transferDate;
            AcceptanceDate = acceptanceDate;
            InitialValue = initialValue;
            UpdatedInitialValue = updatedInitialValue;
        }

        public string CategoryCode { get; }

        public string Description { get; }

        public DateTime TransferDate { get; }

        public DateTime AcceptanceDate { get; }

        public decimal? InitialValue { get; }

        public decimal? UpdatedInitialValue { get; }

        public string DocumentNumber { get; }
    }
}
