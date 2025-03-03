using FluentAssertions;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.Tests.UnitTests.DDD.Event
{
    public class BaseNewCompanyEventTests
    {
        [Test]
        public void Equals_ForNoTaxIdentificationNumberInSource_ReturnsFalse()
        {
            // Arrange
            var company1 = new NewCompanyFromJpkKpirEvent("some company", "00-000 City Street 1/2");
            var company2 = new NewCompanyFromJpkFaEvent("some company", "1234567890", "00-000 City Street 1/2");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ForNoTaxIdentificationNumberInDestination_ReturnsFalse()
        {
            // Arrange
            var company1 = new NewCompanyFromJpkFaEvent("some company", "1234567890", "00-000 City Street 1/2");
            var company2 = new NewCompanyFromJpkKpirEvent("some company", "00-000 City Street 1/2");

            // Act
            var result = company1.Equals(company2);

            // Assert
            result.Should().BeFalse();
        }
    }
}