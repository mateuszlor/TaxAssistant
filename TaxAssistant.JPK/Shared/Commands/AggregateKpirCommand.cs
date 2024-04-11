using TaxAssistant.CQRS;

namespace TaxAssistant.JPK.Shared.Commands
{
    public class AggregateKpirCommand    : ICommand
    {
        public IList<Guid> Ids { get; set; }
    }
}
