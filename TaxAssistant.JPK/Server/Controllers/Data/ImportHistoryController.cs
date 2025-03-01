using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Database;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
	[ApiController]
    [Route("[controller]")]
    public class ImportHistoryController : BaseController<Import>
    {
        public ImportHistoryController(IRepository<Import> repository)
            : base(repository)
        {
        }
    }
}
