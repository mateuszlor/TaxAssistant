using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;

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
            var itemToAdd = new Ewp();

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
    }
}