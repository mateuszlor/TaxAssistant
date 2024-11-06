using Microsoft.Extensions.Logging;
using NSubstitute;
using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Tests.Controller
{
    public class KpirControllerTests : BaseControllerTests<Kpir>
    {
        public KpirControllerTests() : base(new Kpir { Id = Guid.NewGuid() })
        {
            _getSut = () => new KpirController(Substitute.For<ILogger<KpirController>>(), _repository);
        }
    }
}
