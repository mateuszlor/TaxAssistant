using System.ComponentModel.DataAnnotations;

namespace TaxAssistant.JPK.Shared.Model.Abstraction
{
	public abstract class BaseModel : IIdentifiable
	{
		public Guid Id { get; init; } = Guid.NewGuid();
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public bool IsDeleted { get; set; }
		public int Version { get; set; } = 1;
		public DateTime? ModificationDate { get; set; }

		public void IncrementVersion(BaseModel existingItem)
		{
			Version = existingItem.Version + 1;
			CreationDate = existingItem.CreationDate;
			ModificationDate = DateTime.UtcNow;
		}

		public void Delete()
		{
			IsDeleted = true;
		}
	}
}
