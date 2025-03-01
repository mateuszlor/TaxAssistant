using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Address;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.Tests.IntegrationTests
{
    public class CompanyRepositoryTests
    {
        private DatabaseContext _databaseContext;
        private IDomainEventDispatcher _eventDispatcher;
        private ILogger<CompanyRepository> _logger;
        private CompanyRepository _sut;

        [SetUp]
        public void Setup()
        {
            _databaseContext = TestUtils.MakeInMemoryDatabaseContext();

            _eventDispatcher = Substitute.For<IDomainEventDispatcher>();
            _logger = Substitute.For<ILogger<CompanyRepository>>();

            _sut = new CompanyRepository(_databaseContext, _eventDispatcher, _logger);
        }

        [TearDown]
        public void Teardown()
        {
            _databaseContext.Dispose();
        }

        [Test]
        public async Task Add_ShouldSucceed()
        {
            // Arrange
            var itemToAdd = new Company(Origin.JPK, "1234567890", "Monsters Inc.");

            // Act
            var result = await _sut.AddAsync(itemToAdd);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();

            result.Version.Should().Be(1);
            result.IsDeleted.Should().BeFalse();
            result.Events.Should().BeEmpty();
            result.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

            result.Name.Should().Be(itemToAdd.Name);
            result.TaxIdentificationNumber.Should().Be(itemToAdd.TaxIdentificationNumber);
            result.NationalStatisticNumber.Should().Be(itemToAdd.NationalStatisticNumber);

            result.Address.Should().BeNull();
            result.AddressId.Should().BeNull();
            result.ModificationDate.Should().BeNull();

            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Add_WithAddress_ShouldSucceed()
        {
            // Arrange
            var addressToAdd = new Address(Origin.JPK, "Polska", "00-000", "Warszawa", "Prosta", "1", "2");
            var itemToAdd = new Company(Origin.JPK, "1234567890", "Monsters Inc.", addressToAdd);

            // Act
            var result = await _sut.AddAsync(itemToAdd);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();

            result.Version.Should().Be(1);
            result.IsDeleted.Should().BeFalse();
            result.Events.Should().BeEmpty();
            result.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

            result.Name.Should().Be(itemToAdd.Name);
            result.TaxIdentificationNumber.Should().Be(itemToAdd.TaxIdentificationNumber);
            result.NationalStatisticNumber.Should().Be(itemToAdd.NationalStatisticNumber);

            result.ModificationDate.Should().BeNull();

            result.AddressId.Should().NotBeEmpty();
            result.Address.Should().NotBeNull();
            result.Address!.Id.Should().NotBeEmpty();
            result.Address.Country.Should().Be(addressToAdd.Country);
            result.Address.PostalCode.Should().Be(addressToAdd.PostalCode);
            result.Address.City.Should().Be(addressToAdd.City);
            result.Address.BuildingNumber.Should().Be(addressToAdd.BuildingNumber);
            result.Address.LocalNumber.Should().Be(addressToAdd.LocalNumber);
            result.Address.LocalNumber.Should().Be(addressToAdd.LocalNumber);

            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Add_WithEvents_ShouldDispatch()
        {
            // Arrange
            var itemToAdd = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            itemToAdd.Events.Add(new NewCompanyEvent("Monsters Inc.", "1234567890", string.Empty));

            // Act
            var result = await _sut.AddAsync(itemToAdd);

            // Assert
            result.Should().NotBeNull();
            result.Events.Should().BeEmpty();

            await _eventDispatcher.ReceivedWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Add_WithFailedEvents_ShouldThrow()
        {
            // Arrange
            var itemToAdd = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            itemToAdd.Events.Add(new NewCompanyEvent("Monsters Inc.", "1234567890", string.Empty));

            _eventDispatcher.DispatchAsync(Arg.Any<IDomainEvent>()).ThrowsAsync(new Exception("SOME ERROR"));

            // Act & Assert
            await _sut
                .Invoking(x => x.AddAsync(itemToAdd))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Error handling domain event")
                .WithInnerException(typeof(Exception))
                .WithMessage("SOME ERROR");

            // Assert
            _databaseContext.Company.Should().BeEmpty();
        }

        [Test]
        public async Task Update_ForExistingItem_ShouldSucceed()
        {
            // Arrange
            var existingItem = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            await _databaseContext.AddAsync(existingItem);
            await _databaseContext.SaveChangesAsync();

            // Act
            existingItem.Events.Add(new NewCompanyEvent("Monsters Inc.", "1234567890", string.Empty));
            var result = await _sut.UpdateAsync(existingItem);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();
            result.Id.Should().Be(existingItem.Id);

            result.Version.Should().Be(2);
            result.IsDeleted.Should().BeFalse();
            result.Events.Should().BeEmpty();
            result.ModificationDate.Should().NotBe(existingItem.CreationDate);
            result.ModificationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

            await _eventDispatcher.ReceivedWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Update_ForNotExistingItem_ShouldThrow()
        {
            // Arrange
            var notExistingItem = new Company(Origin.JPK, "1234567890", "Monsters Inc.");

            // Act && Assert
            await _sut.Invoking(x => x.UpdateAsync(notExistingItem))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage($"Company with Id='{notExistingItem.Id}' not exist");

            // Assert            
            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Delete_ForExistingItem_ShouldSucceed()
        {
            // Arrange
            var existingItem = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            await _databaseContext.AddAsync(existingItem);
            await _databaseContext.SaveChangesAsync();

            // Act
            await _sut.DeleteAsync(existingItem.Id);

            // Assert
            var result = _databaseContext.Company.SingleOrDefault();

            result.Should().NotBeNull();
            result!.Id.Should().NotBeEmpty();
            result.Id.Should().Be(existingItem.Id);

            result.Version.Should().Be(2);
            result.IsDeleted.Should().BeTrue();
            result.Events.Should().BeEmpty();
            result.ModificationDate.Should().NotBe(existingItem.CreationDate);
            result.ModificationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Delete_ForNotExistingItem_ShouldSucceed()
        {
            // Act
            await _sut.DeleteAsync(Guid.NewGuid());

            // Assert
            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Any_ForExistingItem_ShouldReturnTrue()
        {
            // Arrange
            var existingItem = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            await _databaseContext.AddAsync(existingItem);
            await _databaseContext.SaveChangesAsync();

            // Act
            var result = await _sut.AnyAsync(x => x.Id == existingItem.Id);

            // Assert
            result.Should().BeTrue();

            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task Any_ForNotExistingItem_ShouldReturnTrue()
        {
            // Arrange
            var existingItem = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            await _databaseContext.AddAsync(existingItem);
            await _databaseContext.SaveChangesAsync();

            // Act
            var result = await _sut.AnyAsync(x => x.Id == Guid.NewGuid());

            // Assert
            result.Should().BeFalse();

            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }

        [Test]
        public async Task GetAll_ShouldReturnNotDeletedOnly()
        {
            // Arrange
            var existingItem1 = new Company(Origin.JPK, "1234567890", "Monsters Inc.");
            await _databaseContext.AddAsync(existingItem1);
            await _databaseContext.SaveChangesAsync();

            var existingItem2 = new Company(Origin.JPK, "1111222233", "Evil Corp");
            await _databaseContext.AddAsync(existingItem2);
            await _databaseContext.SaveChangesAsync();

            await _sut.DeleteAsync(existingItem1.Id);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);

            var item1 = result[0];
            item1.Id.Should().Be(existingItem2.Id);

            await _eventDispatcher.DidNotReceiveWithAnyArgs().DispatchAsync(Arg.Any<IDomainEvent>());
        }
    }
}