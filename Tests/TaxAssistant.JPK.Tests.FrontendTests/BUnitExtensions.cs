using AngleSharp.Dom;

namespace TaxAssistant.JPK.Tests.FrontendTests
{
    public static class BUnitExtensions
    {
        public static IElement FindById(this IRenderedFragment component, string id)
        {
            return component.Find("[id='" + id + "']");
        }

        public static string? GetInputValue(this IElement element)
        {
            return element.GetAttribute("value");
        }
    }
}
