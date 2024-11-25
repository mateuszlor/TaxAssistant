using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Database.Fa;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
	[ApiController]
    [Route("[controller]")]
    public class FaController : BaseController<Fa>
    {
        public FaController(
            ILogger<FaController> logger,
            IRepository<Fa>repository)
            : base(logger, repository)
        {
        }
    }
}
