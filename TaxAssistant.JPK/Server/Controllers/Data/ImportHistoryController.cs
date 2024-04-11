using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Model.Database;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
    [ApiController]
    [Route("[controller]")]
    public class ImportHistoryController : BaseController<Import>
    {

        public ImportHistoryController(
            ILogger<ImportHistoryController> logger,
            ImportRepository repository)
            : base(logger, repository)
        {
        }
    }
}
