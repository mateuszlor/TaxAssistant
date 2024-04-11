namespace TaxAssistant.JPK.Shared.Model.Application
{
    public class BaseApplicationModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
