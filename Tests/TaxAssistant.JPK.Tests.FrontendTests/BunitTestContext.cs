using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace TaxAssistant.JPK.Tests.FrontendTests;

/// <summary>
/// Test context wrapper for bUnit.
/// Read more about using <see cref="BunitTestContext"/> <seealso href="https://bunit.dev/docs/getting-started/writing-tests.html#remove-boilerplate-code-from-tests">here</seealso>.
/// </summary>
public abstract class BunitTestContext : IDisposable
{
    protected BunitContext Context { get; private set; } = default!;

    protected IServiceCollection Services => Context.Services;

    [SetUp]
    [SetCulture("pl-PL")]
    public virtual void Setup()
    {
        Context = new BunitContext();
        CultureInfo.CurrentUICulture =
            CultureInfo.CurrentCulture =
            new CultureInfo("pl-PL");
    }

    [TearDown]
    public void TearDown() => Context?.Dispose();

    public void Dispose() => Context?.Dispose();

    protected IRenderedComponent<T> Render<T>(Action<ComponentParameterCollectionBuilder<T>> parameterBuilder = null) where T : IComponent
        => Context.Render(parameterBuilder);
}
