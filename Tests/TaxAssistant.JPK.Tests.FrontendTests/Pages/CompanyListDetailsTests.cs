using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using FluentAssertions;
using FluentAssertions.BUnit;
using NSubstitute;
using TaxAssistant.JPK.Client.Clients.Abstraction;
using TaxAssistant.JPK.Client.Pages;
using TaxAssistant.JPK.Client.Shared;
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
        cut.FindById("address").GetInputValue().Should().BeNullOrEmpty();

        cut.FindByDataTestId("delete-button").Should().HaveAttribute("disabled", "");
        cut.FindByDataTestId("edit-button").Attributes["disabled"].Should().BeNull();

        cut.FindByDataTestId("synchronize-button").TextContent.Should().Be("Check on VAT WhiteList");
        cut.FindAll("button").Should().NotContain(x => x.TextContent.Equals("Save"));
    }

    [Test]
    public void CompanyListDetails_ForFoundWithAddress_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var address = new Shared.Model.Domain.Address.Address(Origin.VatWhiteList, "Poland", "12-345", "Szczebrzeszyn", "Przek¹tna", "12", "34");
        var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.", address, "111122220");

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
        cut.FindById("address").GetInputValue().Should().Be("Przek¹tna 12/34\n12-345 Szczebrzeszyn\nPoland");
    }

    [Test]
    public void CompanyListDetails_ForEditMode_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.", "111122220");

        _companyClient!.GetAsync(guid).Returns(Task.FromResult<Company?>(company));

        // Act
        var cut = RenderComponent<CompanyListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Edit.ToString()));

        // Assert
        cut.FindByDataTestId("delete-button").Attributes["disabled"].Should().BeNull();
        cut.FindByDataTestId("edit-button").Should().HaveAttribute("disabled", "");

        cut.FindAll("button").Should().NotContain(x => x.TextContent.Equals("Check on VAT WhiteList"));
        cut.FindByDataTestId("save-button").TextContent.Should().Be("Save");
    }

    [Test]
    public void CompanyListDetails_ForEditClicked_ShouldChangeMode()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.", "111122220");

        _companyClient!.GetAsync(guid).Returns(Task.FromResult<Company?>(company));

        // Act
        var cut = RenderComponent<CompanyListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Preview.ToString()));

        cut.FindByDataTestId("edit-button").Click();

        // Assert
        cut.FindByDataTestId("delete-button").Attributes["disabled"].Should().BeNull();
        cut.FindByDataTestId("edit-button").Should().HaveAttribute("disabled", "");

        cut.FindAll("button").Should().NotContain(x => x.TextContent.Equals("Check on VAT WhiteList"));
        cut.FindByDataTestId("save-button").TextContent.Should().Be("Save");
    }

    [Test]
    public void CompanyListDetails_ForSaveClicked_ShouldChangeMode()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.", "111122220");

        _companyClient!.GetAsync(guid).Returns(Task.FromResult<Company?>(company));
        _companyClient!.UpdateAsync(Arg.Any<Company>()).Returns(Task.FromResult<Company?>(company));

        // Act
        var cut = RenderComponent<CompanyListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Edit.ToString()));

        cut.FindByDataTestId("save-button").Click();

        // Assert
        _companyClient!.ReceivedWithAnyArgs(1).UpdateAsync(Arg.Any<Company>());

        cut.FindByDataTestId("delete-button").Should().HaveAttribute("disabled", "");
        cut.FindByDataTestId("edit-button").Attributes["disabled"].Should().BeNull();

        cut.FindByDataTestId("synchronize-button").TextContent.Should().Be("Check on VAT WhiteList");
        cut.FindAll("button").Should().NotContain(x => x.TextContent.Equals("Save"));
    }

    [Test]
    public void CompanyListDetails_ForSynchronizeClicked_ShouldCallApi()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.", "111122220");

        _companyClient!.GetAsync(guid).Returns(Task.FromResult<Company?>(company));
        _companyClient!.GetAsync(company.Id, "synchronizeWithVatWhiteList").Returns(Task.FromResult<Company?>(company));

        // Act
        var cut = RenderComponent<CompanyListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Preview.ToString()));

        cut.FindByDataTestId("synchronize-button").Click();

        // Assert
        _companyClient!.Received(1).GetAsync(company.Id, "synchronizeWithVatWhiteList");

        cut.FindByDataTestId("delete-button").Should().HaveAttribute("disabled", "");
        cut.FindByDataTestId("edit-button").Attributes["disabled"].Should().BeNull();

        cut.FindByDataTestId("synchronize-button").TextContent.Should().Be("Check on VAT WhiteList");
        cut.FindAll("button").Should().NotContain(x => x.TextContent.Equals("Save"));
    }
}
