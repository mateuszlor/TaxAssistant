using AngleSharp.Dom;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace TaxAssistant.JPK.Tests.FrontendTests;

public class ElementAssertions(IElement subject) : ReferenceTypeAssertions<IElement, ElementAssertions>(subject)
{
    protected override string Identifier => "element";

    public AndConstraint<ElementAssertions> HaveClass(string expected, string because = "", params object[] becauseArgs)
    {
        Subject.ClassList.Should().Contain(expected, because, becauseArgs);
        return new AndConstraint<ElementAssertions>(this);
    }

    public AndConstraint<ElementAssertions> HaveChildMarkup(string expected, string because = "", params object[] becauseArgs)
    {
        Subject.InnerHtml.Should().Contain(expected, because, becauseArgs);
        return new AndConstraint<ElementAssertions>(this);
    }

    public AndConstraint<ElementAssertions> HaveAttribute(string name, string expected, string because = "", params object[] becauseArgs)
    {
        Subject.Attributes[name]?.Value.Should().Be(expected, because, becauseArgs);
        return new AndConstraint<ElementAssertions>(this);
    }
}
