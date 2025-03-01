using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public class KpirControllerTests : BaseControllerTests<Kpir>
    {
        public KpirControllerTests() : base(new Kpir())
        {
            _getSut = () => new KpirController(_repository);
        }
    }
}
