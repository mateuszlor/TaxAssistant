using FluentAssertions;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events;

namespace TaxAssistant.JPK.Tests.Domain
{
    public class InvoiceTests
    {
        private Invoice _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Invoice(Origin.JPK, "FV/2024/01/01");
        }

        [Test]
        public void SetBuyer_ForValidData_ShouldSucceed()
        {
            // Arrange
            var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.");

            // Act
            _sut.SetBuyer(company);

            // Assert
            _sut.Buyer.Should().NotBeNull();
            _sut.Events.OfType<BuyerSetEvent>().Should().HaveCount(1);
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetBuyer_ForAlreadyExistingValue_ShouldThrow()
        {
            // Arrange
            var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            _sut.SetBuyer(company);

            // Act && Assert
            _sut
                .Invoking(x => x.SetBuyer(company))
                .Should()
                .Throw<InvalidOperationException>();

            // Assert
            _sut.Buyer.Should().NotBeNull();
            _sut.Events.OfType<BuyerSetEvent>().Should().HaveCount(1); // previously added
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetBuyer_ForNull_ShouldThrow()
        {
            // Act && Assert
            _sut
                .Invoking(x => x.SetBuyer(null))
                .Should()
                .Throw<InvalidOperationException>();

            // Assert
            _sut.Buyer.Should().BeNull();
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetSeller_ForValidData_ShouldSucceed()
        {
            // Arrange
            var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.");

            // Act
            _sut.SetSeller(company);

            // Assert
            _sut.Seller.Should().NotBeNull();
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().HaveCount(1);
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetSeller_ForAlreadyExistingValue_ShouldThrow()
        {
            // Arrange
            var company = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            _sut.SetSeller(company);

            // Act && Assert
            _sut
                .Invoking(x => x.SetSeller(company))
                .Should()
                .Throw<InvalidOperationException>();

            // Assert
            _sut.Seller.Should().NotBeNull();
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().HaveCount(1); // previously added
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetSeller_ForNull_ShouldThrow()
        {
            // Act && Assert
            _sut
                .Invoking(x => x.SetSeller(null))
                .Should()
                .Throw<InvalidOperationException>();

            // Assert
            _sut.Seller.Should().BeNull();
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetAmountValues_ForZeroTotalNetValue_ShouldThrow()
        {
            // Act && Assert
            _sut
                .Invoking(x => x.SetAmountValues(0, 1, 1))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("TotalNetValue cannot be 0");

            // Assert
            _sut.TotalNetValue.Should().Be(0);
            _sut.TotalGrossValue.Should().Be(0);
            _sut.TotalVat.Should().Be(0);
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetAmountValues_ForZeroTotalGrossValue_ShouldThrow()
        {
            // Act && Assert
            _sut
                .Invoking(x => x.SetAmountValues(1, 0, 1))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("TotalGrossValue cannot be 0");

            // Assert
            _sut.TotalNetValue.Should().Be(0);
            _sut.TotalGrossValue.Should().Be(0);
            _sut.TotalVat.Should().Be(0);
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetAmountValues_ForInvalidAmounts_ShouldThrow()
        {
            // Act && Assert
            _sut
                .Invoking(x => x.SetAmountValues(1, 1, 1))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Net/gross/VAT amounts does not match");

            // Assert
            _sut.TotalNetValue.Should().Be(0);
            _sut.TotalGrossValue.Should().Be(0);
            _sut.TotalVat.Should().Be(0);
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetAmountValues_ForValidAmountsWithVat_ShouldSucceed()
        {
            // Act
            _sut.SetAmountValues(100, 123, 23);

            // Assert
            _sut.TotalNetValue.Should().Be(100);
            _sut.TotalGrossValue.Should().Be(123);
            _sut.TotalVat.Should().Be(23);
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().HaveCount(1);
        }

        [Test]
        public void SetAmountValues_ForValidAmountsWithoutVat_ShouldSucceed()
        {
            // Act
            _sut.SetAmountValues(100, 100, 0);

            // Assert
            _sut.TotalNetValue.Should().Be(100);
            _sut.TotalGrossValue.Should().Be(100);
            _sut.TotalVat.Should().Be(0);
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().HaveCount(1);
        }

        [Test]
        public void SetAmountValue_ForZeroTotalNetValue_ShouldThrow()
        {
            // Act && Assert
            _sut
                .Invoking(x => x.SetAmountValue(0))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("TotalNetValue cannot be 0");

            // Assert
            _sut.TotalNetValue.Should().Be(0);
            _sut.TotalGrossValue.Should().Be(0);
            _sut.TotalVat.Should().Be(0);
            _sut.Events.OfType<BuyerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<SellerSetEvent>().Should().BeEmpty();
            _sut.Events.OfType<AmountValuesSetEvent>().Should().BeEmpty();
        }

        [Test]
        public void SetDeliveryDate_ForAlreadyExistingDate_ShouldThrow()
        {
            // Act && Assert
            _sut.SetDeliveryDate(DateTime.Today);

            _sut
                .Invoking(x => x.SetDeliveryDate(DateTime.Today.AddDays(10)))
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Delivery date already set");

            // Assert
            _sut.DeliveryDate.Should().Be(DateTime.Today);
            _sut.Events.OfType<DeliveryDateSetEvent>().Should().HaveCount(1); // once only (initial set)
        }

        [Test]
        public void SetIssueDate_ForAlreadyExistingDate_ShouldThrow()
        {
            // Act && Assert
            _sut.SetIssueDate(DateTime.Today);

            _sut
                .Invoking(x => x.SetIssueDate(DateTime.Today.AddDays(10)))
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Issue date already set");

            // Assert
            _sut.IssueDate.Should().Be(DateTime.Today);
            _sut.Events.OfType<IssueDateSetEvent>().Should().HaveCount(1); // once only (initial set)
        }
    }
}