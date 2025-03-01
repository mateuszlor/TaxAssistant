using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Domain.Events;

namespace TaxAssistant.JPK.Tests.IntegrationTests
{
    public class EwpRepositoryTests
    {
        private DatabaseContext _databaseContext;
        private IDomainEventDispatcher _eventDispatcher;
        private ILogger<EwpRepository> _logger;
        private EwpRepository _sut;

        [SetUp]
        public void Setup()
        {
            _databaseContext = TestUtils.MakeInMemoryDatabaseContext();

            _eventDispatcher = Substitute.For<IDomainEventDispatcher>();
            _logger = Substitute.For<ILogger<EwpRepository>>();

            _sut = new EwpRepository(_databaseContext, _eventDispatcher, _logger);
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
            var itemToAdd = new Ewp
            {
                FixedAssets = [
                    new EwpFixedAsset
                    {
                        Number = 1,
                        DocumentNumber = "ST/1",
                        AcceptanceDate = DateTime.Today.AddYears(-1),
                        TransferDate = DateTime.Today.AddYears(-1),
                        Description = "my awesome car",
                        InitialValue = 100_000,
                        UpdatedInitialValue = 100_000,
                        DepreciationRate = 0.4M,
                        CategoryCode = "741"
                    }
                ]
            };
            var addedEvents = new List<IDomainEvent>();
            await _eventDispatcher.DispatchAsync(Arg.Do<IDomainEvent>(addedEvents.Add));

            // Act
            var result = await _sut.AddAsync(itemToAdd);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();

            result.Version.Should().Be(1);
            result.IsDeleted.Should().BeFalse();
            result.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

            result.Events.Should().BeEmpty();

            addedEvents.Should().HaveCount(1);
            addedEvents.OfType<NewFixedAssetEvent>().Should().HaveCount(1);
            
            var fixedAssetEvent = addedEvents.OfType<NewFixedAssetEvent>().Single();
            fixedAssetEvent.DocumentNumber.Should().Be("ST/1");
            fixedAssetEvent.InitialValue.Should().Be(100_000);
            fixedAssetEvent.UpdatedInitialValue.Should().Be(100_000);
            fixedAssetEvent.Description.Should().Be("my awesome car");
            fixedAssetEvent.CategoryCode.Should().Be("741");
            fixedAssetEvent.AcceptanceDate.Should().Be(DateTime.Today.AddYears(-1));
            fixedAssetEvent.TransferDate.Should().Be(DateTime.Today.AddYears(-1));

            await _eventDispatcher.Received(1).DispatchAsync(Arg.Any<IDomainEvent>());
        }
    }
}