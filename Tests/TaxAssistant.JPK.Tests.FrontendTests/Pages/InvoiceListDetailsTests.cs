using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using FluentAssertions;
using NSubstitute;
using TaxAssistant.JPK.Client.Clients.Abstraction;
using TaxAssistant.JPK.Client.Pages;
using TaxAssistant.JPK.Client.Shared;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.Tests.FrontendTests.Pages;

public class InvoiceListDetailsTests : BunitTestContext
{
    private IApiClient<Invoice>? _InvoiceClient;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _InvoiceClient = Substitute.For<IApiClient<Invoice>>();
        Services.AddSingleton(_InvoiceClient);
    }

    [Test]
    public void InvoiceListDetails_ForNotFoundInvoice_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var cut = Render<InvoiceListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Preview.ToString()));

        // Assert
        cut.Markup.Should().Contain("Invoice not found");
    }

    [Test]
    public void InvoiceListDetails_ForMinimumInformation_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var invoice = new Invoice(Origin.JPK, "FV 2020/01/01", null, null, 0, 0, 0, false, null, null);

        _InvoiceClient!.GetAsync(guid).Returns(Task.FromResult<Invoice?>(invoice));

        // Act
        var cut = Render<InvoiceListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Preview.ToString()));

        // Assert
        cut.FindByDataTestId("header").TextContent.Should().Be("FV 2020/01/01");

        cut.FindByDataTestId("issueDate").GetInnerText().Trim().Should().Be("Issue date");
        cut.FindByDataTestId("deliveryDate").GetInnerText().Trim().Should().Be("Delivery date");

        cut.FindByDataTestId("buyer").GetInnerText().Should().Be("Buyer:");
        cut.FindByDataTestId("seller").GetInnerText().Should().Be("Seller:");

        cut.FindByDataTestId("summary-net").GetInnerText().Should().Be("Total net 0,00");
        cut.FindByDataTestId("summary-vat").GetInnerText().Should().Be("Total VAT 0,00");
        cut.FindByDataTestId("summary-gross").GetInnerText().Should().Be("Total gross 0,00");

        cut.FindByDataTestId("rowsTable").GetElementsByTagName("tbody").Single().GetElementsByTagName("tr").Should().HaveCount(0);
    }

    [Test]
    public void InvoiceListDetails_ForFoundWithDatesAndAmounts_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var invoice = new Invoice(Origin.JPK, "FV 2020/01/01", null, null, 10000, 12300, 2300, true, new DateTime(2020, 02, 15), new DateTime(2020, 01, 31));

        _InvoiceClient!.GetAsync(guid).Returns(Task.FromResult<Invoice?>(invoice));

        // Act
        var cut = Render<InvoiceListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Preview.ToString()));

        // Assert
        cut.FindByDataTestId("header").TextContent.Should().Be("FV 2020/01/01");

        cut.FindByDataTestId("issueDate").GetInnerText().Should().Be("Issue date 15.02.2020");
        cut.FindByDataTestId("deliveryDate").GetInnerText().Should().Be("Delivery date 31.01.2020");

        cut.FindByDataTestId("buyer").GetInnerText().Should().Be("Buyer:");
        cut.FindByDataTestId("seller").GetInnerText().Should().Be("Seller:");

        cut.FindByDataTestId("summary-net").GetInnerText().Should().Be("Total net 10 000,00");
        cut.FindByDataTestId("summary-vat").GetInnerText().Should().Be("Total VAT 2 300,00");
        cut.FindByDataTestId("summary-gross").GetInnerText().Should().Be("Total gross 12 300,00");

        cut.FindByDataTestId("rowsTable").GetElementsByTagName("tbody").Single().GetElementsByTagName("tr").Should().HaveCount(0);
    }


    [Test]
    public void InvoiceListDetails_ForFoundWithRows_ShouldDisplay()
    {
        // Arrange
        var guid = Guid.NewGuid();

        var invoice = new Invoice(Origin.JPK, "FV 2020/01/01", null, null, 10000, 12300, 2300, true, new DateTime(2020, 02, 15), new DateTime(2020, 01, 31));
        invoice.AddRow(new InvoiceRow(Origin.JPK, invoice.Id)
        {
            Name = "Something",
            MetricUnit = "pcs",
            Count = 10,
            VatRate = Shared.Model.Database.Fa.Enum.VatRate.Item23,
            UnitPriceNet = 100,
            UnitPriceGross = 123,
            TotalPriceNet = 1000,
            TotalPriceGross = 1230
        });

        _InvoiceClient!.GetAsync(guid).Returns(Task.FromResult<Invoice?>(invoice));

        // Act
        var cut = Render<InvoiceListDetails>(parameters => parameters
            .Add(x => x.IdParameter, guid.ToString())
            .Add(x => x.FormModeParameter, FormMode.Preview.ToString()));

        // Assert
        cut.FindByDataTestId("header").TextContent.Should().Be("FV 2020/01/01");

        cut.FindByDataTestId("issueDate").GetInnerText().Should().Be("Issue date 15.02.2020");
        cut.FindByDataTestId("deliveryDate").GetInnerText().Should().Be("Delivery date 31.01.2020");

        cut.FindByDataTestId("buyer").GetInnerText().Should().Be("Buyer:");
        cut.FindByDataTestId("seller").GetInnerText().Should().Be("Seller:");

        cut.FindByDataTestId("summary-net").GetInnerText().Should().Be("Total net 10 000,00");
        cut.FindByDataTestId("summary-vat").GetInnerText().Should().Be("Total VAT 2 300,00");
        cut.FindByDataTestId("summary-gross").GetInnerText().Should().Be("Total gross 12 300,00");

        var headRow = cut.FindByDataTestId("rowsTable").GetElementsByTagName("thead").Single().FindChild<IHtmlTableRowElement>();
        headRow?.Should().NotBeNull();

        headRow!.Children.Should().HaveCount(9);
        headRow!.Children[0].InnerHtml.Should().Be("Lp.");
        headRow!.Children[1].InnerHtml.Should().Be("Item");
        headRow!.Children[2].InnerHtml.Should().Be("Metric unit");
        headRow!.Children[3].InnerHtml.Should().Be("Count");
        headRow!.Children[4].InnerHtml.Should().Be("Unit price net");
        headRow!.Children[5].InnerHtml.Should().Be("Total price net");
        headRow!.Children[6].InnerHtml.Should().Be("VAT rate");
        headRow!.Children[7].InnerHtml.Should().Be("VAT");
        headRow!.Children[8].InnerHtml.Should().Be("Total price gross");

        var rows = cut.FindByDataTestId("rowsTable").GetElementsByTagName("tbody").Single().GetElementsByTagName("tr");
        rows.Should().HaveCount(1);

        var cells = rows.Single().GetElementsByTagName("td");
        cells.Should().HaveCount(9);
        cells[0].InnerHtml.Should().Be("1");
        cells[1].InnerHtml.Should().Be("Something");
        cells[2].InnerHtml.Should().Be("pcs");
        cells[3].InnerHtml.Should().Be("10");
        cells[4].InnerHtml.Trim().Should().Be("100,00");
        cells[5].InnerHtml.Trim().Should().Be("1 000,00");
        cells[6].InnerHtml.Should().Be("23%");
        cells[7].InnerHtml.Trim().Should().Be("230,00");
        cells[8].InnerHtml.Trim().Should().Be("1 230,00");
    }
}
