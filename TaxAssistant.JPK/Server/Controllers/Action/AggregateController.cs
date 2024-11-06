using Microsoft.AspNetCore.Mvc;
using TaxAssistant.CQRS;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model;

namespace TaxAssistant.JPK.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AggregateController : ControllerBase
    {
        private readonly IGate _gate;

        public AggregateController(IGate gate)
        {
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
