using FluentAssertions;
using TaxAssistant.JPK.Client.Shared.Generic;

namespace TaxAssistant.JPK.Tests.FrontendTests.Generic;

public class LoadingTests : BunitTestContext
{
    [Test]
    public void Loading_ShouldDisplay()
    {
        // Act
        var cut = Render<Loading>();

        // Assert
        cut.Find("div").Should().HaveClass("lds-ring");
        cut.Find("div").TextContent.Should().BeNullOrWhiteSpace();
    }
}
