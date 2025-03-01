using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain.Events
{
    class PropertyValueChangedEvent : IDomainEvent
    {
        public Guid ItemId { get; init; }
        public string? ItemType { get; init; }
        public required string PropertyName { get; init; }
        public object? OldValue { get; init; }
        public object? NewValue { get; init; }
    }
}
