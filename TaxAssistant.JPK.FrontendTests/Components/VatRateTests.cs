using FluentAssertions;
using FluentAssertions.BUnit;

namespace TaxAssistant.JPK.FrontendTests.Components;

public class VatRateTests : BunitTestContext
{
	[TestCase(Shared.Model.Database.Fa.VatRate.Item0, "0%")]
	[TestCase(Shared.Model.Database.Fa.VatRate.Item3, "3%")]
	[TestCase(Shared.Model.Database.Fa.VatRate.Item4, "4%")]
	[TestCase(Shared.Model.Database.Fa.VatRate.Item5, "5%")]
	[TestCase(Shared.Model.Database.Fa.VatRate.Item7, "7%")]
	[TestCase(Shared.Model.Database.Fa.VatRate.Item8, "8%")]
	[TestCase(Shared.Model.Database.Fa.VatRate.Item22, "22%")]
	[TestCase(Shared.Model.Database.Fa.VatRate.Item23, "23%")]
	[TestCase(Shared.Model.Database.Fa.VatRate.np, "np")]
	[TestCase(Shared.Model.Database.Fa.VatRate.oo, "oo")]
	[TestCase(Shared.Model.Database.Fa.VatRate.zw, "zw")]
	public void VatRate_ShouldDisplay(Shared.Model.Database.Fa.VatRate enumValue, string displayedValue)
	{
		// Act
		var cut = base.RenderComponent((ComponentParameterCollectionBuilder<Client.Shared.Components.VatRate> parameters) => parameters.Add(x => x.Rate, enumValue));

		// Assert
		cut.Markup.Should().Be(displayedValue);
	}
}
