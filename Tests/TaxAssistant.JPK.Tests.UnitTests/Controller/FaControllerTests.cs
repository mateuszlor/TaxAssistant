using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Model.Database.Fa;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public class FaControllerTests : BaseControllerTests<Fa>
    {
        public FaControllerTests() : base(new Fa())
        {
            _getSut = () => new FaController(_repository);
        }
    }
}
