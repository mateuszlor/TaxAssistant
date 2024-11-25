using FluentAssertions;
using FluentAssertions.BUnit;
using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;

namespace TaxAssistant.JPK.Tests.FrontendTests.Components;

public class VatRateTests : BunitTestContext
{
    [TestCase(VatRate.Item0, "0%")]
    [TestCase(VatRate.Item3, "3%")]
    [TestCase(VatRate.Item4, "4%")]
    [TestCase(VatRate.Item5, "5%")]
    [TestCase(VatRate.Item7, "7%")]
    [TestCase(VatRate.Item8, "8%")]
    [TestCase(VatRate.Item22, "22%")]
    [TestCase(VatRate.Item23, "23%")]
    [TestCase(VatRate.np, "np")]
    [TestCase(VatRate.oo, "oo")]
    [TestCase(VatRate.zw, "zw")]
    public void VatRate_ShouldDisplay(VatRate enumValue, string displayedValue)
    {
        // Act
        var cut = base.RenderComponent((ComponentParameterCollectionBuilder<Client.Shared.Components.VatRate> parameters) => parameters.Add(x => x.Rate, enumValue));

        // Assert
        cut.Markup.Should().Be(displayedValue);
    }
}
