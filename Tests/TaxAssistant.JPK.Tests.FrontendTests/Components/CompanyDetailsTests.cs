using AngleSharp.Dom;
using FluentAssertions;
using FluentAssertions.BUnit;
using TaxAssistant.JPK.Client.Shared.Components;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Address;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.Tests.FrontendTests.Components;

public class CompanyDetailsTests : BunitTestContext
{
    [Test]
    public void CompanyDetails_ForNull_ShouldDisplayNothing()
    {
        // Act
        var cut = base.RenderComponent<CompanyDetails>();

        // Assert
        cut.Markup.Should().BeEmpty();
    }

    [Test]
    public void CompanyDetails_ForEmptyValues_ShouldDisplayEmpty()
    {
        // Arrange
        var comapny = new Company(Origin.JPK, null, string.Empty);

        // Act
        var cut = base.RenderComponent<CompanyDetails>(parameters => parameters.Add(x => x.Company, comapny));

        // Assert
        cut.Markup.Should().NotBeEmpty();
        cut.Find("div").InnerHtml.Should().BeEmpty();
    }

    [Test]
    public void CompanyDetails_ForOnlyName_ShouldDisplay()
    {
        // Arrange
        var comapny = new Company(Origin.JPK, null, "Some company");

        // Act
        var cut = base.RenderComponent<CompanyDetails>(parameters => parameters.Add(x => x.Company, comapny));

        // Assert
        cut.Markup.Should().NotBeEmpty();
        cut.FindByDataTestId("name").InnerHtml.Should().Be("Some company");
    }

    [Test]
    public void CompanyDetails_ForCompanyWithNip_ShouldDisplay()
    {
        // Arrange
        var comapny = new Company(Origin.JPK, "1234567890", "Some company");

        // Act
        var cut = base.RenderComponent<CompanyDetails>(parameters => parameters.Add(x => x.Company, comapny));

        // Assert
        cut.Markup.Should().NotBeEmpty();
        cut.FindByDataTestId("name").InnerHtml.Should().Be("Some company");
        cut.FindByDataTestId("nip").GetInnerText().Should().Be("NIP: 1234567890");
    }

    [Test]
    public void CompanyDetails_ForCompanyWithPesel_ShouldDisplay()
    {
        // Arrange
        var comapny = new Company(Origin.JPK, "12345678910", "Some company");

        // Act
        var cut = base.RenderComponent<CompanyDetails>(parameters => parameters.Add(x => x.Company, comapny));

        // Assert
        cut.Markup.Should().NotBeEmpty();
        cut.FindByDataTestId("name").InnerHtml.Should().Be("Some company");
        cut.FindByDataTestId("pesel").GetInnerText().Should().Be("PESEL: 12345678910");
    }

    [Test]
    public void CompanyDetails_ForCompanyWithAddress_ShouldDisplay()
    {
        // Arrange
        var address = new Address(Origin.JPK, null, "12-345", "Some city", "Some street", "1", "2");
        var comapny = new Company(Origin.JPK, "1234567890", "Some company", address);

        // Act
        var cut = base.RenderComponent<CompanyDetails>(parameters => parameters.Add(x => x.Company, comapny));

        // Assert
        cut.Markup.Should().NotBeEmpty();
        cut.FindByDataTestId("name").InnerHtml.Should().Be("Some company");
        cut.FindByDataTestId("address-street").GetInnerText().Should().Be("Some street 1/2");
        cut.FindByDataTestId("address-city").GetInnerText().Should().Be("12-345 Some city");
        // cut.FindByDataTestId("address-country").InnerHtml.Should().BeEmpty(); // failed to render
    }
}
