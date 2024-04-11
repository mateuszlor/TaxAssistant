using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
    [ApiController]
    [Route("[controller]")]
    public class KpirController : BaseController<Kpir>
    {
        public KpirController(
            ILogger<KpirController> logger,
            KpirRepository repository)
            : base(logger, repository)
        {
        }
    }
}
