using FluentAssertions;
using FluentAssertions.BUnit;
using TaxAssistant.JPK.Client.Shared.Generic;

namespace TaxAssistant.JPK.Tests.FrontendTests.Generic;

public class CollapsableSectionTests : BunitTestContext
{
	[Test]
	public void CollapsableSection_ShouldDisplay()
	{
		// Act
		var cut = RenderComponent<CollapsableSection>(p => p
			.Add(x => x.Title, "click")
			.Add(x => x.Collapsed, true)
			.AddChildContent("inner text"));

		// Assert
		cut.Find("div").Should().HaveClass("accordion");
		cut.Find("div").FirstElementChild?.Should().HaveClass("card");

		cut.FindByDataTestId("card-header").Should().HaveClass("card-header");
		cut.FindByDataTestId("card-header")?.FirstElementChild?.Should().HaveClass("oi-plus");
		cut.FindByDataTestId("card-header").TextContent.Should().Be("click");
	}

	[Test]
	public void CollapsableSection_OnClick_ShouldUnfold()
	{
		// Act
		var cut = RenderComponent<CollapsableSection>(p => p
			.Add(x => x.Title, "click")
			.Add(x => x.Collapsed, true)
			.AddChildContent("inner text"));

		cut.FindByDataTestId("card-header").Click();

		// Assert
		cut.FindByDataTestId("card-header")?.FirstElementChild?.Should().HaveClass("oi-minus");

		cut.FindByDataTestId("card-body").Should().HaveClass("card-body");
		cut.FindByDataTestId("card-body").TextContent.Should().Be("inner text");
	}
}
