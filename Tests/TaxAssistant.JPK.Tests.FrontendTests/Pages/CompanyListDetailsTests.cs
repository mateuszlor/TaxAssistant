using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using FluentAssertions;
using FluentAssertions.BUnit;
using NSubstitute;
using TaxAssistant.JPK.Client.Clients.Abstraction;
using TaxAssistant.JPK.Client.Pages;
using TaxAssistant.JPK.Client.Shared;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.Tests.FrontendTests.Pages;

public class CompanyListDetailsTests : BunitTestContext
{
    private IApiClient<Company>? _companyClient;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _companyClient = Substitute.For<IApiClient<Company>>();
        Services.AddSingleton(_companyClient);
    }

    [Test]
    public void CompanyListDetails_ForNotFoundInvoice_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var cut = RenderComponent<CompanyListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Preview.ToString()));

        // Assert
        cut.Markup.Should().Contain("Company not found");
    }

    [Test]
    public void CompanyListDetails_ForFoundWithoutAddress_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.", "111122220");

        _companyClient!.GetAsync(guid).Returns(Task.FromResult<Company?>(company));

        // Act
        var cut = RenderComponent<CompanyListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Preview.ToString()));

        // Assert
        cut.FindByDataTestId("header").TextContent.Should().Be("Company - details");
        cut.FindByDataTestId("form").FindChild<IHtmlFieldSetElement>()!.IsDisabled.Should().BeTrue();
        cut.FindById("name").GetInputValue().Should().Be("Monsters Inc.");
        cut.FindById("taxIdentificationNumber").GetInputValue().Should().Be("1234567890");
        cut.FindById("nationalStatisticNumber").GetInputValue().Should().Be("111122220");
        cut.FindById("origin").GetInputValue().Should().Be("JPK");
    }
}
