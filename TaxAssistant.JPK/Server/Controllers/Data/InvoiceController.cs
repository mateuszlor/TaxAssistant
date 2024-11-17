using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
	[ApiController]
	[Route("[controller]")]
	public class InvoiceController : BaseController<Invoice>
	{
		public InvoiceController(
			ILogger<InvoiceController> logger,
			IRepository<Invoice> repository)
			: base(logger, repository)
		{
		}
	}
}
