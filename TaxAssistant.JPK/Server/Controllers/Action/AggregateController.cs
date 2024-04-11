using Microsoft.AspNetCore.Mvc;
using TaxAssistant.CQRS;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Adapter;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model;

namespace TaxAssistant.JPK.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AggregateController : ControllerBase
    {
        private readonly ILogger<AggregateController> _logger;
        private readonly KpirAdapter _adapter;
        private readonly KpirRepository _kpirRepository;
        private readonly ImportRepository _importRepository;
        private readonly Gate _gate;

        public AggregateController(
            ILogger<AggregateController> logger,
            KpirAdapter adapter,
            KpirRepository kpirRepository,
            ImportRepository importRepository,
            Gate gate)
        {
            _logger = logger;
            _adapter = adapter;
            _kpirRepository = kpirRepository;
            _importRepository = importRepository;
            _gate = gate;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ImportResult), 200)]
        [ProducesResponseType(typeof(ImportResult), 400)]
        public async Task<IActionResult> Aggregate([FromBody] AggregateKpirCommand command)
        {
            try
            {
                var result = await _gate.HandleAsync<AggregateKpirCommand, AggregateKpirCommandResult>(command);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
