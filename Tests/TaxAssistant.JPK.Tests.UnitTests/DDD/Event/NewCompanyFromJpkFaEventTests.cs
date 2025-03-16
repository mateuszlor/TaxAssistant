using FluentAssertions;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.Tests.UnitTests.DDD.Event
{
    public class NewCompanyFromJpkFaEventTests
    {
        [Test]
        public void Equals_ForDifferentObjectType_ReturnsFalse()
        {
            // Arrange
            var company = new NewCompanyFromJpkFaEvent("some company", "1234567890", "00-000 City Street 1/2");

            // Act
            var result = company.Equals(new object());

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForNullObject_ReturnsFalse()
        {
            // Arrange
            var company = new NewCompanyFromJpkFaEvent("some company", "1234567890", "00-000 City Street 1/2");

            // Act
            var result = company.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForSameTaxIdentificationNumber_ReturnsTrue()
        {
            // Arrange
            var company1 = new NewCompanyFromJpkFaEvent("some company", "1234567890", "00-000 City Street 1/2");
            var company2 = new NewCompanyFromJpkFaEvent("some company with other name", "1234567890", "01-000 OtherCity OtherStreet 3/4");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ForDifferentTaxIdentificationNumber_ReturnsTrue()
        {
            // Arrange
            var company1 = new NewCompanyFromJpkFaEvent("some company", "1234567890", "00-000 City Street 1/2");
            var company2 = new NewCompanyFromJpkFaEvent("some company", "1234567899", "00-000 City Street 1/2");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeFalse();
        }
    }
}