using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.Shared.Extensions;
using TaxAssistant.JPK.Shared.Model.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain.Events;

namespace TaxAssistant.JPK.Shared.Model.Domain
{
    public abstract class BaseDomainModel : BaseModel, IAggregate
    {
        [JsonIgnore]
        public IList<IDomainEvent> Events { get; } = [];

        public Origin Origin { get; init; }

        protected BaseDomainModel(Origin origin)
        {
            Origin = origin;
        }

        protected void ChangeProperty<T>(Expression<Func<T, object?>> propertySelector, object? newValue, bool allowNull = false)
            where T : BaseDomainModel
        {
            var property = propertySelector.GetPropertyInfo();

            if (!allowNull && newValue == null)
            {
                return;
            }

            var oldValue = property.GetValue(this);

            if (oldValue == newValue)
            {
                return;
            }

            property.SetValue(this, newValue);

            Events.Add(new PropertyValueChangedEvent
            {
                ItemId = Id,
                ItemType = GetType().FullName,
                PropertyName = property.Name,
                OldValue = oldValue,
                NewValue = newValue
            });
        }
    }
}
