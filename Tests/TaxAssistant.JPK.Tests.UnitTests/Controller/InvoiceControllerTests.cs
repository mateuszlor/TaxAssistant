using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public class InvoiceControllerTests : BaseControllerTests<Invoice>
    {
        public InvoiceControllerTests() : base(new Invoice(Origin.JPK, "FV/2024/01/01"))
        {
            _getSut = () => new InvoiceController(_repository);
        }
    }
}
