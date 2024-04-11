using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Shared.Commands
{
    public class AggregateKpirCommandResult
    {
        public Kpir AggregatedKpir { get; set; }

        public IList<Kpir> SourceKpirs { get; set; }

        public IList<string> Warnings { get; set; } = new List<string>();
    }
}
