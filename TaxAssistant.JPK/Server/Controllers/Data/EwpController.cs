using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
    [ApiController]
    [Route("[controller]")]
    public class EwpController : BaseController<Ewp>
    {
        public EwpController(
            ILogger<EwpController> logger,
            EwpRepository repository)
            : base(logger, repository)
        {
        }
    }
}
