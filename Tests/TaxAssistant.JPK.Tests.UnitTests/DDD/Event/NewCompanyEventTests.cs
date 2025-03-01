using FluentAssertions;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.Tests.UnitTests.DDD.Event
{
    public class NewCompanyEventTests
    {
        [Test]
        public void Equals_ForDifferentObjectType_ReturnsFalse()
        {
            // Arrange
            var company = new NewCompanyEvent("some company", "1234567890", "00-000 City Street 1/2");

            // Act
            var result = company.Equals(new object());

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForNullObject_ReturnsFalse()
        {
            // Arrange
            var company = new NewCompanyEvent("some company", "1234567890", "00-000 City Street 1/2");

            // Act
            var result = company.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForSameTaxIdentificationNumber_ReturnsTrue()
        {
            // Arrange
            var company1 = new NewCompanyEvent("some company", "1234567890", "00-000 City Street 1/2");
            var company2 = new NewCompanyEvent("some company with other name", "1234567890", "01-000 OtherCity OtherStreet 3/4");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ForDifferentTaxIdentificationNumber_ReturnsTrue()
        {
            // Arrange
            var company1 = new NewCompanyEvent("some company", "1234567890", "00-000 City Street 1/2");
            var company2 = new NewCompanyEvent("some company", "1234567899", "00-000 City Street 1/2");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForSameNameAndAddress_ReturnsTrue()
        {
            // Arrange
            var company1 = new NewCompanyEvent("some company", "00-000 City Street 1/2");
            var company2 = new NewCompanyEvent("some company", "00-000 City Street 1/2");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ForDifferentNameAndSameAddress_ReturnsFalse()
        {
            // Arrange
            var company1 = new NewCompanyEvent("some company", "00-000 City Street 1/2");
            var company2 = new NewCompanyEvent("some company with other name", "00-000 City Street 1/2");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForSameNameAndDifferentAddress_ReturnsFalse()
        {
            // Arrange
            var company1 = new NewCompanyEvent("some company", "00-000 City Street 1/2");
            var company2 = new NewCompanyEvent("some company", "01-000 OtherCity OtherStreet 3/4");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForNoTaxIdentificationNumberInSource_ReturnsFalse()
        {
            // Arrange
            var company1 = new NewCompanyEvent("some company", "00-000 City Street 1/2");
            var company2 = new NewCompanyEvent("some company", "1234567890", "00-000 City Street 1/2");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForNoTaxIdentificationNumberInDestination_ReturnsFalse()
        {
            // Arrange
            var company1 = new NewCompanyEvent("some company", "1234567890", "00-000 City Street 1/2");
            var company2 = new NewCompanyEvent("some company", "00-000 City Street 1/2");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Constructor_ForTaxIdentificationNumberWithoutPrefixProvided_ShouldSplit()
        {
            // Act
            var result = new NewCompanyEvent($"Monsters Inc.{Environment.NewLine}1234567890", "00-000 City Street 1/2");

            // Assert
            result.Should().NotBeNull();
            result.CompanyData.Should().Be($"Monsters Inc.{Environment.NewLine}1234567890");
            result.CompanyName.Should().Be("Monsters Inc.");
            result.TaxIdentificationNumber.Should().Be("1234567890");
            result.Address.Should().Be("00-000 City Street 1/2");
        }

        [Test]
        public void Constructor_ForTaxIdentificationNumberWithNipPrefixProvided_ShouldSplit()
        {
            // Act
            var result = new NewCompanyEvent($"Monsters Inc.{Environment.NewLine}NIP: 1234567890", "00-000 City Street 1/2");

            // Assert
            result.Should().NotBeNull();
            result.CompanyData.Should().Be($"Monsters Inc.{Environment.NewLine}NIP: 1234567890");
            result.CompanyName.Should().Be("Monsters Inc.");
            result.TaxIdentificationNumber.Should().Be("1234567890");
            result.Address.Should().Be("00-000 City Street 1/2");
        }

        [Test]
        public void Constructor_ForTaxIdentificationNumberWithPeselPrefixProvided_ShouldSplit()
        {
            // Act
            var result = new NewCompanyEvent($"John Doe{Environment.NewLine}PESEL: 12345678910", "00-000 City Street 1/2");

            // Assert
            result.Should().NotBeNull();
            result.CompanyData.Should().Be($"John Doe{Environment.NewLine}PESEL: 12345678910");
            result.CompanyName.Should().Be("John Doe");
            result.TaxIdentificationNumber.Should().Be("12345678910");
            result.Address.Should().Be("00-000 City Street 1/2");
        }

        [Test]
        public void Constructor_ForSplittedData_ShouldMerge()
        {
            // Act
            var result = new NewCompanyEvent("Monsters Inc.", "1234567890", "00-000 City Street 1/2");

            // Assert
            result.Should().NotBeNull();
            result.CompanyData.Should().Be($"Monsters Inc.{Environment.NewLine}NIP: 1234567890");
            result.CompanyName.Should().Be("Monsters Inc.");
            result.TaxIdentificationNumber.Should().Be("1234567890");
            result.Address.Should().Be("00-000 City Street 1/2");
        }

        [Test]
        public void Constructor_ForNullCompanyData_ShouldThrow()
        {
            // Act && Assert
            var action = () => new NewCompanyEvent(null, "00-000 City Street 1/2");
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ForEmptyCompanyData_ShouldThrow()
        {
            // Act && Assert
            var action = () => new NewCompanyEvent(string.Empty, "00-000 City Street 1/2");
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ForWhitespaceCompanyData_ShouldThrow()
        {
            // Act && Assert
            var action = () => new NewCompanyEvent("   ", "00-000 City Street 1/2");
            action.Should().Throw<ArgumentNullException>();
        }
    }
}