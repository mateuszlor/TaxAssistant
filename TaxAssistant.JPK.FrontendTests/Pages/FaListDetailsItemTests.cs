using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.BUnit;
using NSubstitute;
using TaxAssistant.JPK.Client.Clients.Abstraction;
using TaxAssistant.JPK.Client.Pages;
using TaxAssistant.JPK.Shared.Model.Database.Fa;

namespace TaxAssistant.JPK.FrontendTests.Generic;

public class FaListDetailsItemTests : BunitTestContext
{
    private IApiClient<Fa>? _faClient;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _faClient = Substitute.For<IApiClient<Fa>>();
        Services.AddSingleton(_faClient);
    }

    [Test]
    public void FaListDetailsItem_ForNotFoundInvoice_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var invoiceNumber = "FV-2024-01-01";

        // Act
        var cut = RenderComponent<FaListDetailsItem>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.InvoiceNumberParameter, invoiceNumber));

        // Assert
        cut.Markup.Should().Contain("Invoice not found");
    }

    [Test]
    public void FaListDetailsItem_ForFoundInvoice_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var invoiceNumber = "FV/2024/01/01";
        var invoiceNumberEscaped = "FV-2024-01-01";

        var fa = new Fa
        {
            Invoices = 
            [
                new FaInvoice 
                {
                    DocumentNumber = invoiceNumber,
                    Rows = []
                }
            ]
        };

        _faClient.GetAsync(guid).Returns(Task.FromResult<Fa?>(fa));

        // Act
        var cut = RenderComponent<FaListDetailsItem>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.InvoiceNumberParameter, invoiceNumberEscaped));

        // Assert
        cut.Markup.Should().Contain(invoiceNumber);
    }
}
