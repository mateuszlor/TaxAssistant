using AngleSharp.Dom;

namespace TaxAssistant.JPK.Tests.FrontendTests;

public static class BunitElementAssertionsExtensions
{

    public static ElementAssertions Should(this IElement element) => new(element);
}
