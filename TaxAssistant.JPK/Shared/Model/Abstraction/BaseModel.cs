namespace TaxAssistant.JPK.Shared.Model.Abstraction
{
	public abstract class BaseModel : IIdentifiable
	{
		public Guid Id { get; } = Guid.NewGuid();
		public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
		public bool IsDeleted { get; protected set; }
		public int Version { get; private set; } = 1;
		public DateTime? ModificationDate { get; private set; }

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
