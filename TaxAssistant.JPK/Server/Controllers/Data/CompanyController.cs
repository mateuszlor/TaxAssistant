using Microsoft.AspNetCore.Mvc;
using TaxAssistant.CQRS.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : BaseController<Company>
    {
        private readonly IGate _gate;

        public CompanyController(
            IRepository<Company> repository,
            IGate gate)
            : base(repository)
        {
            _gate = gate ?? throw new ArgumentNullException(nameof(gate));
        }

        [HttpGet("{id}/synchronizeWithVatWhiteList")]
        public async Task<IActionResult> SynchronizeWithVatWhiteList(Guid id)
        {
            try
            {
                var result = await _gate.HandleAsync<SynchronizeWithVatWhiteListCommand, SynchronizeWithVatWhiteListCommandResult>(new SynchronizeWithVatWhiteListCommand(id));

                return result.Company == null
                    ? BadRequest(result.Error)
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
