using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using FluentAssertions;
using NSubstitute;
using TaxAssistant.JPK.Client.Clients.Abstraction;
using TaxAssistant.JPK.Client.Pages;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;

namespace TaxAssistant.JPK.Tests.FrontendTests.Pages;

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
        var invoiceNumberEscaped = "FV_2024-01-01";

        // Act
        var cut = Render<FaListDetailsItem>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.InvoiceNumberParameter, invoiceNumberEscaped));

        // Assert
        cut.Markup.Should().Contain("Invoice not found");
    }

    [Test]
    public void FaListDetailsItem_ForFoundEmptyInvoice_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var invoiceNumber = "FV 2024/01/01";
        var invoiceNumberEscaped = "FV_2024-01-01";

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

        _faClient!.GetAsync(guid).Returns(Task.FromResult<Fa?>(fa));

        // Act
        var cut = Render<FaListDetailsItem>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.InvoiceNumberParameter, invoiceNumberEscaped));

        // Assert
        cut.Markup.Should().Contain(invoiceNumber);
        cut.FindByDataTestId("header").TextContent.Should().Be(invoiceNumber);
    }

    [Test]
    public void FaListDetailsItem_ForFoundInvoiceWithRows_ShouldDisplay()
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
                    TotalVatBaseRate = 10,
                    TotalVatRate8 = 10,
                    TotalVatRate5 = 10,
                    TotalPriceNetVatExempted = 30,
                    IssueDate = new DateTime(2024, 01, 10),
                    DeliveryDate = new DateTime(2024, 01, 01),
                    Rows = [
                        new FaInvoiceRow { Name = "ITEM 1", Count = 2, MetricUnit = "kg", UnitPriceNet = 5, TotalPriceNet = 10, VatRate = VatRate.Item23, TotalPriceGross = 12.3M },
                        new FaInvoiceRow { Name = "ITEM 2", Count = 3, MetricUnit = "m", UnitPriceNet = 3.33M, TotalPriceNet = 10, VatRate = VatRate.Item8, TotalPriceGross = 10.8M },
                        new FaInvoiceRow { Name = "ITEM 3", UnitPriceNet = 2.5M, TotalPriceNet = 10, VatRate = VatRate.Item5, TotalPriceGross = 10.5M }
                        ]
                }
            ]
        };

        _faClient!.GetAsync(guid).Returns(Task.FromResult<Fa?>(fa));

        // Act
        var cut = Render<FaListDetailsItem>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.InvoiceNumberParameter, invoiceNumberEscaped));

        // Assert
        cut.Markup.Should().Contain(invoiceNumber);
        cut.FindByDataTestId("header").TextContent.Should().Be(invoiceNumber);

        cut.FindByDataTestId("issueDate").LastChild?.TextContent.Should().Be("10.01.2024");

        cut.FindByDataTestId("deliveryDate").LastChild?.TextContent.Should().Be("01.01.2024");

        var rows = cut.FindByDataTestId("rowsTable").GetElementsByTagName("tbody").Children(null as string).ToList();
        rows.Should().HaveCount(3);

        for (var i = 0; i < rows.Count; i++)
        {
            var invoiceRow = fa.Invoices.Single().Rows[i];

            rows[i].ChildNodes[0].FirstChild?.TextContent.Trim().Should().Be(invoiceRow.Name);
            rows[i].ChildNodes[1].FirstChild?.TextContent.Trim().Should().Be(invoiceRow.MetricUnit);
            rows[i].ChildNodes[2].FirstChild?.TextContent.Trim().Should().Be(invoiceRow.Count.ToString());
            rows[i].ChildNodes[3].FirstChild?.TextContent.Trim().Should().Be($"{invoiceRow.UnitPriceNet:##0.00}");
            rows[i].ChildNodes[4].FirstChild?.TextContent.Trim().Should().Be($"{invoiceRow.TotalPriceNet:##0.00}");
            rows[i].ChildNodes[5].FirstChild?.TextContent.Trim().Should().Be($"{invoiceRow.TotalPriceGross:##0.00}");
        }
    }
}
