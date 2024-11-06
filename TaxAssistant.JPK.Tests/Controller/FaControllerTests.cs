using Microsoft.Extensions.Logging;
using NSubstitute;
using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Model.Database.Fa;

namespace TaxAssistant.JPK.Tests.Controller
{
    public class FaControllerTests : BaseControllerTests<Fa>
    {
        public FaControllerTests() : base(new Fa { Id = Guid.NewGuid() })
        {
            _getSut = () => new FaController(Substitute.For<ILogger<FaController>>(), _repository);
        }
    }
}
