using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
	[ApiController]
    [Route("[controller]")]
    public class EwpController : BaseController<Ewp>
    {
        public EwpController(IRepository<Ewp> repository)
            : base(repository)
        {
        }
    }
}
