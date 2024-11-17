using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
	[ApiController]
	[Route("[controller]")]
	public class CompanyController : BaseController<Company>
	{
		public CompanyController(
			ILogger<CompanyController> logger,
			IRepository<Company> repository)
			: base(logger, repository)
		{
		}
	}
}
