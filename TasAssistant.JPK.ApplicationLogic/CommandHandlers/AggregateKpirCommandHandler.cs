using System.Data.SqlTypes;
using TaxAssistant.CQRS.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;
using TaxAssistant.JPK.Shared.Model.Database.Shared;

namespace TaxAssistant.JPK.ApplicationLogic.CommandHandlers
{
	public class AggregateKpirCommandHandler : ICommandHandler<AggregateKpirCommand, AggregateKpirCommandResult>
	{
		private readonly IRepository<Kpir> _repository;

		public AggregateKpirCommandHandler(IRepository<Kpir> repository)
		{
			_repository = repository;
		}

		public Task<AggregateKpirCommandResult> HandleAsync(AggregateKpirCommand? command)
		{
			var result = new AggregateKpirCommandResult();

			var kpirs = command == null
				? []
				: command.Ids
					.AsParallel()
					.Select(x => _repository.GetAsync(x))
					.Select(x => x.Result)
					.Where(x => x != null)
					.Select(x => x!)
					.ToList();

			if (!kpirs.Any())
			{
				result.Warnings.Add("No source KPiRs");

				return Task.FromResult(result);
			}

			result.SourceKpirs = kpirs;
			var aggregatedRows = AggregateRows(kpirs);

			if (!aggregatedRows.Any())
			{
				result.Warnings.Add("No KPiR rows");

				return Task.FromResult(result);
			}

			var revenue = aggregatedRows
				.Where(x => x.RevenueTotal.HasValue)
				.Sum(x => x.RevenueTotal!.Value);

			var cost = aggregatedRows
				.Where(x => x.CostTotal.HasValue)
				.Sum(x => x.CostTotal!.Value);

			result.AggregatedKpir = new Kpir
			{
				Rows = aggregatedRows,
				PhysicalInventories = AggregatePhysicalInventories(kpirs),
				ControlData = new KpirControlData
				{
					RowCount = aggregatedRows.Count,
					TotalIncome = revenue,
				},
				Header = AggregateHeaders(kpirs),
				Summary = new KpirSummary
				{
					TotalCost = cost,
					TotalIncome = revenue - cost,
					PhysicalInventoryYearStart = kpirs.Min(x => x.Summary?.PhysicalInventoryYearStart ?? 0),
					PhysicalInventoryYearEnd = kpirs.Max(x => x.Summary?.PhysicalInventoryYearEnd ?? 0)
				}
			};

			return Task.FromResult(result);
		}

		private static KpirHeader AggregateHeaders(List<Kpir> kpirs)
		{
			var firstKpir = kpirs.First();
			var headers = kpirs.Where(x => x.Header != null).Select(x => x.Header).ToList();

			var dateFrom = SqlDateTime.MinValue;
			var dateTo = SqlDateTime.MinValue;

			if (headers.Count > 0)
			{
				dateFrom = headers.Min(x => x.DateFrom);
				dateTo = headers.Max(x => x.DateTo);
			}
			var header = new KpirHeader
			{
				Currency = firstKpir.Header?.Currency ?? string.Empty,
				FormCode = firstKpir.Header?.FormCode ?? string.Empty,
				FormVariant = firstKpir.Header?.FormVariant ?? 0,
				Purpose = JpkPurpose.FirstTime,
				TaxOfficeCode = firstKpir.Header?.TaxOfficeCode ?? string.Empty,
				DateFrom = dateFrom.Value,
				DateTo = dateTo.Value
			};
			return header;
		}

		private static List<KpirRow> AggregateRows(List<Kpir> kpirs)
		{
			var rows = kpirs
				.Where(x => x.Rows != null)
				.SelectMany(x => x.Rows)
				.OrderBy(x => x.Date)
				.ThenBy(x => x.Number)
				.ToList();

			for (var i = 0; i < rows.Count; i++)
			{
				rows[i].Number = i + 1;
				rows[i].Kpir = null;
				rows[i].KpirId = Guid.Empty;
			}

			return rows;
		}

		private static List<KpirPhysicalInventory> AggregatePhysicalInventories(List<Kpir> kpirs)
		{
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

			return aggregatedPhysicalInventories;
		}
	}
}
