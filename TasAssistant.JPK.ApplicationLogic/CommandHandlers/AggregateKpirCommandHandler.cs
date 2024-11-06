using System.Data.SqlTypes;
using TaxAssistant.CQRS;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TasAssistant.JPK.ApplicationLogic.CommandHandlers
{
    public class AggregateKpirCommandHandler : ICommandHandler<AggregateKpirCommand, AggregateKpirCommandResult>
    {
        private readonly IRepository<Kpir> _repository;

        public AggregateKpirCommandHandler(IRepository<Kpir> repository)
        {
            _repository = repository;
        }

        public async Task<AggregateKpirCommandResult> HandleAsync(AggregateKpirCommand command)
        {
            var kpirs = command == null
                ? new List<Kpir>()
                : command.Ids
                    .AsParallel()
                    .Select(_repository.GetAsync)
                    .Select(x => x.Result)
                    .Where(x => x != null)
                    .Select(x => x!)
                    .ToList();

            var firstKpir = kpirs.FirstOrDefault();

            var aggregatedRows = kpirs
                .Where(x => x.Rows != null)
                .SelectMany(x => x.Rows)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Number)
                .ToList();

            Kpir? aggregatedKpir = null;

            if (aggregatedRows.Any())
            {
                for (var i = 0; i < aggregatedRows.Count; i++)
                {
                    aggregatedRows[i].Number = i + 1;
                    aggregatedRows[i].Kpir = null;
                    aggregatedRows[i].KpirId = Guid.Empty;
                }

                var revenue = aggregatedRows
                    .Where(x => x.RevenueTotal.HasValue)
                    .Sum(x => x.RevenueTotal.Value);

                var cost = aggregatedRows
                    .Where(x => x.CostTotal.HasValue)
                    .Sum(x => x.CostTotal.Value);

                var income = revenue - cost;

                var aggregatedPhysicalInventories = kpirs
                    .Where(x => x.PhysicalInventories != null)
                    .SelectMany(x => x.PhysicalInventories)
                    .OrderBy(x => x.Date)
                    .ToList();

                for (var i = 0; i < aggregatedPhysicalInventories.Count; i++)
                {
                    aggregatedPhysicalInventories[i].Kpir = null;
                    aggregatedPhysicalInventories[i].KpirId = Guid.Empty;
                }

                var headers = kpirs.Where(x => x.Header != null).Select(x => x.Header).ToList();

                var dateFrom = SqlDateTime.MinValue;
                var dateTo = SqlDateTime.MinValue;

                if (headers.Count > 0)
                {
                    dateFrom = headers.Min(x => x.DateFrom);
                    dateTo = headers.Max(x => x.DateTo);
                }

                aggregatedKpir = new Kpir
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
                        DateFrom = dateFrom.Value,
                        DateTo = dateTo.Value
                    },
                    Summary = new KpirSummary
                    {
                        TotalCost = cost,
                        TotalIncome = income,
                        PhysicalInventoryYearStart = 0, // TODO: what to insert here?
                        PhysicalInventoryYearEnd = 0 // TODO: what to insert here?
                    }
                };
            }

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
