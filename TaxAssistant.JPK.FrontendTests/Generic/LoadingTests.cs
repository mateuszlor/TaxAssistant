using FluentAssertions;
using FluentAssertions.BUnit;
using TaxAssistant.JPK.Client.Shared.Generic;

namespace TaxAssistant.JPK.FrontendTests.Generic;

public class LoadingTests : BunitTestContext
{
    [Test]
    public void Loading_ShouldDisplay()
    {
        // Act
        var cut = RenderComponent<Loading>();

        // Assert
        cut.Find("div").Should().HaveClass("lds-ring");
        cut.Find("div").Should().HaveChildMarkup(@"<div />");
        cut.Find("div").TextContent.Should().BeNullOrWhiteSpace();
    }
}
