using System.Linq.Expressions;
using NSubstitute;
using TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.Tests.UnitTests.DDD.EventHandler
{
    public class NewCompanyFromJpkEwpEventHandlerTests
    {
        private IRepository<Company> _repository;
        private NewCompanyFromJpkEwpEventHandler _sut;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository<Company>>();
            _sut = new NewCompanyFromJpkEwpEventHandler(_repository);
        }

        [Test]
        public async Task HandleAsync_ForNewCompany_ShouldAdd()
        {
            // Arrange
            var companyEvent = new NewCompanyFromJpkEwpEvent("some company", "1234567890", "00-000", "City", "Street", "1", "2", "Voivodeship");

            // Act
            await _sut.HandleAsync(companyEvent);

            // Assert
            await _repository.Received().AddAsync(Arg.Any<Company>());
        }

        [Test]
        public async Task HandleAsync_ForExistingCompanyByTaxIdentificationNumber_ShouldSkip()
        {
            // Arrange
            _repository.AnyAsync(Arg.Any<Expression<Func<Company, bool>>>()).Returns(true);
            var companyEvent = new NewCompanyFromJpkEwpEvent("some company", "1234567890", "00-000", "City", "Street", "1", "2", "Voivodeship");

            // Act
            await _sut.HandleAsync(companyEvent);

            // Assert
            await _repository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Company>());
        }

        [Test]
        public async Task HandleAsync_ForExistingCompanyByName_ShouldSkip()
        {
            // Arrange
            _repository.AnyAsync(Arg.Any<Expression<Func<Company, bool>>>()).Returns(false, true);
            var companyEvent = new NewCompanyFromJpkEwpEvent("some company", "1234567890", "00-000", "City", "Street", "1", "2", "Voivodeship");

            // Act
            await _sut.HandleAsync(companyEvent);

            // Assert
            await _repository.DidNotReceiveWithAnyArgs().AddAsync(Arg.Any<Company>());
        }
    }
}