using AngleSharp.Dom;

namespace TaxAssistant.JPK.Tests.FrontendTests;

public static partial class BunitExtensions
{
    public static IElement FindByDataTestId(this IRenderedComponent<Microsoft.AspNetCore.Components.IComponent> cut, string testId)
        => cut.Find($"[data-test-id='{testId}']");

    public static IElement FindById(this IRenderedComponent<Microsoft.AspNetCore.Components.IComponent> cut, string id)
        => cut.Find($"#{id}");

    public static string GetInnerText(this IElement element)
    {
        var text = element.TextContent;
        // Specifically handle various hidden characters
        text = text.Replace("\u200E", ""); // LRM
        text = text.Replace("\uFEFF", ""); // BOM/ZWNBSP
        // Remove ALL whitespace for extreme robustness
        return System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ").Trim();
    }

    public static string GetInputValue(this IElement element)
    {
        if (element is AngleSharp.Html.Dom.IHtmlInputElement input)
            return input.Value;
        if (element is AngleSharp.Html.Dom.IHtmlTextAreaElement textArea)
            return textArea.Value;
        return element.TextContent;
    }
}
