using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;
using TaxAssistant.JPK.Shared.Model.Domain.Events;

namespace TaxAssistant.JPK.Tests.IntegrationTests
{
    public class FaRepositoryTests
    {
        private DatabaseContext _databaseContext;
        private IDomainEventDispatcher _eventDispatcher;
        private ILogger<FaRepository> _logger;
        private FaRepository _sut;

        [SetUp]
        public void Setup()
        {
            _databaseContext = TestUtils.MakeInMemoryDatabaseContext();

            _eventDispatcher = Substitute.For<IDomainEventDispatcher>();
            _logger = Substitute.For<ILogger<FaRepository>>();

            _sut = new FaRepository(_databaseContext, _eventDispatcher, _logger);
        }

        [TearDown]
        public void Teardown()
        {
            _databaseContext.Dispose();
        }

        [Test]
        public async Task Add_ForEmpty_ShouldSucceed()
        {
            // Arrange
            var itemToAdd = new Fa();

            // Act
            var result = await _sut.AddAsync(itemToAdd);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();

            result.Version.Should().Be(1);
            result.IsDeleted.Should().BeFalse();
            result.Events.Should().BeEmpty();
            result.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Add_IvoiceWithoutCompanies_ShouldThrow()
        {
            // Arrange
            var itemToAdd = new Fa
            {
                Invoices =
                [
                    new FaInvoice()
                ]
            };

            // Act
            await _sut
                .Invoking(x => x.AddAsync(itemToAdd))
                .Should()
                .ThrowAsync<Exception>();

            // Assert
            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Add_IvoiceWithoutRows_ShouldThrow()
        {
            // Arrange
            var itemToAdd = new Fa
            {
                Invoices =
                [
                    new FaInvoice
                    {
                        Currency = "PLN",
                        Seller = new FaInvoiceCompany(),
                        Buyer = new FaInvoiceCompany()
                    }
                ]
            };

            // Act
            await _sut
                .Invoking(x => x.AddAsync(itemToAdd))
                .Should()
                .ThrowAsync<Exception>();

            // Assert
            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Add_ForValidIvoice_ShouldSucceed()
        {
            // Arrange
            var itemToAdd = new Fa
            {
                Invoices =
                [
                    new FaInvoice
                    {
                        Currency = "PLN",
                        Seller = new FaInvoiceCompany(), 
                        Buyer = new FaInvoiceCompany(),
                        Rows =
                        [
                            new FaInvoiceRow
                            {
                                Name = "Something"
                            }
                        ]
                    }
                ]
            };

            // Act
            var result = await _sut.AddAsync(itemToAdd);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();

            result.Version.Should().Be(1);
            result.IsDeleted.Should().BeFalse();
            result.Events.Should().BeEmpty();
            result.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

            await _eventDispatcher.Received(3).DispatchAsync(Arg.Any<IDomainEvent>());
            await _eventDispatcher.Received(2).DispatchAsync(Arg.Any<NewCompanyFromJpkFaEvent>());
            await _eventDispatcher.Received(1).DispatchAsync(Arg.Any<NewInvoiceFromJpkFaEvent>());
        }
    }
}