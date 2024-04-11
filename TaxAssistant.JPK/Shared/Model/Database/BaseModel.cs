namespace TaxAssistant.JPK.Shared.Model.Database
{
    public class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public int Version { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
