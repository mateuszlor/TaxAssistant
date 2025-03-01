using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public class EwpControllerTests : BaseControllerTests<Ewp>
    {
        public EwpControllerTests() : base(new Ewp())
        {
            _getSut = () => new EwpController(_repository);
        }
    }
}
