using NSubstitute;
using TaxAssistant.CQRS.Abstraction;
using TaxAssistant.JPK.Server.Controllers.Data;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.Tests.UnitTests.Controller
{
    public class CompanyControllerTests : BaseControllerTests<Company>
    {
        public CompanyControllerTests() : base(new Company(Origin.JPK, "1234567890", "Monsters Inc."))
        {
            var gate = Substitute.For<IGate>();
            _getSut = () => new CompanyController(_repository, gate);
        }
    }
}
