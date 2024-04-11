using TaxAssistant.CQRS;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TasAssistant.JPK.ApplicationLogic.CommandHandlers
{
    public class AggregateKpirCommandHandler : ICommandHandler<AggregateKpirCommand, AggregateKpirCommandResult>
    {
        private readonly KpirRepository _repository;

        public AggregateKpirCommandHandler(KpirRepository repository)
        {
            _repository = repository;
        }

        public async Task<AggregateKpirCommandResult> HandleAsync(AggregateKpirCommand command)
        {
            var kpirs = command.Ids
                .AsParallel()
                .Select(_repository.GetAsync)
                .Select(x => x.Result)
                .ToList();

            var aggregatedRows = kpirs
                .SelectMany(x => x.Rows)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Number)
                .ToList();

            for (var i = 0; i < aggregatedRows.Count; i++)
            {
                aggregatedRows[i].Number = i + 1;
                aggregatedRows[i].Kpir = null;
                aggregatedRows[i].KpirId = Guid.Empty;
            }

            var firstKpir = kpirs.FirstOrDefault();

            var revenue = aggregatedRows
                .Where(x => x.RevenueTotal.HasValue)
                .Sum(x => x.RevenueTotal.Value);

            var cost = aggregatedRows
                .Where(x => x.CostTotal.HasValue)
                .Sum(x => x.CostTotal.Value);

            var income = revenue - cost;

            var aggregatedPhysicalInventories = kpirs
                .SelectMany(x => x.PhysicalInventories)
                .OrderBy(x => x.Date)
                .ToList();

            for (var i = 0; i < aggregatedPhysicalInventories.Count; i++)
            {
                aggregatedPhysicalInventories[i].Kpir = null;
                aggregatedPhysicalInventories[i].KpirId = Guid.Empty;
            }

            var aggregatedKpir = new Kpir
            {
                Rows = aggregatedRows,
                PhysicalInventories = aggregatedPhysicalInventories,
                ControlData = new KpirControlData
                {
                    RowCount = aggregatedRows.Count,
                    TotalIncome = revenue,
                },
                Header = new KpirHeader
                {
                    Currency = firstKpir?.Header?.Currency,
                    FormCode = firstKpir?.Header?.FormCode,
                    FormVariant = firstKpir?.Header?.FormVariant ?? 0,
                    Purpose = TaxAssistant.JPK.Shared.Model.Database.Kpir.Enum.KpirPurpose.FirstTime,
                    TaxOfficeCode = firstKpir?.Header?.TaxOfficeCode,
                    DateFrom = kpirs.Min(x => x.Header.DateFrom),
                    DateTo = kpirs.Max(x => x.Header.DateTo)
                },
                Summary = new KpirSummary
                {
                    TotalCost = cost,
                    TotalIncome = income,
                    PhysicalInventoryYearStart = 0, // TODO: what to insert here?
                    PhysicalInventoryYearEnd = 0 // TODO: what to insert here?
                }
            };

            var result = new AggregateKpirCommandResult
            {
                AggregatedKpir = aggregatedKpir,
                SourceKpirs = kpirs
            };

            if (firstKpir == null)
            {
                result.Warnings.Add("No source KPiRs");
            }
            else if (!aggregatedRows.Any())
            {
                result.Warnings.Add("No KPiR rows");
            }

            return result;
        }
    }
}
