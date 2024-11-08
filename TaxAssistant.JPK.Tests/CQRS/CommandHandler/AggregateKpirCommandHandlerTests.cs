using System;
using FluentAssertions;
using NSubstitute;
using TasAssistant.JPK.ApplicationLogic.CommandHandlers;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Tests.CQRS.CommandHandler
{
    public class AggregateKpirCommandHandlerTests
    {
        private IRepository<Kpir> _repository;
        private AggregateKpirCommandHandler _sut;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IRepository<Kpir>>();
            _sut = new AggregateKpirCommandHandler(_repository);
        }

        [Test]
        public async Task HandleAsync_ForNullCommand_ShouldReturnWarning()
        {
            // Act
            var result = await _sut.HandleAsync(null);

            // Assert
            result.Should().NotBeNull();
            result.AggregatedKpir.Should().BeNull();
            result.SourceKpirs.Should().BeEmpty();
            result.Warnings.Should().HaveCount(1);
            result.Warnings.Single().Should().Be("No source KPiRs");
        }

        [Test]
        public async Task HandleAsync_ForNotFoundIds_ShouldReturnWarning()
        {
            // Arrange
            var command = new AggregateKpirCommand
            {
                Ids = [
                    Guid.NewGuid(),
                    Guid.NewGuid()
                    ]
            };

            // Act
            var result = await _sut.HandleAsync(command);

            // Assert
            result.Should().NotBeNull();
            result.AggregatedKpir.Should().BeNull();
            result.SourceKpirs.Should().BeEmpty();
            result.Warnings.Should().HaveCount(1);
            result.Warnings.Single().Should().Be("No source KPiRs");
        }

        [Test]
        public async Task HandleAsync_ForSourceWithNullRows_ShouldReturnWarning()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var command = new AggregateKpirCommand
            {
                Ids = [guid]
            };

            var kpir = new Kpir();
            _repository.GetAsync(guid).Returns(Task.FromResult<Kpir?>(kpir));

            // Act
            var result = await _sut.HandleAsync(command);

            // Assert
            result.Should().NotBeNull();
            result.AggregatedKpir.Should().BeNull();
            result.SourceKpirs.Should().HaveCount(1);
            result.Warnings.Should().HaveCount(1);
            result.Warnings.Single().Should().Be("No KPiR rows");
        }

        [Test]
        public async Task HandleAsync_ForSourceWithNoRows_ShouldReturnWarning()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var command = new AggregateKpirCommand
            {
                Ids = [guid]
            };

            var kpir = new Kpir
            {
                Rows = new List<KpirRow>()
            };

            _repository.GetAsync(guid).Returns(Task.FromResult<Kpir?>(kpir));

            // Act
            var result = await _sut.HandleAsync(command);

            // Assert
            result.Should().NotBeNull();
            result.AggregatedKpir.Should().BeNull();
            result.SourceKpirs.Should().HaveCount(1);
            result.Warnings.Should().HaveCount(1);
            result.Warnings.Single().Should().Be("No KPiR rows");
		}

		[Test]
		public async Task HandleAsync_ForSourcesWithRowsButNoContent_ShouldSucceeded()
		{
			// Arrange
			var guid1 = Guid.NewGuid();
			var guid2 = Guid.NewGuid();
			var command = new AggregateKpirCommand
			{
				Ids = [guid1, guid2]
			};

			var kpir1 = new Kpir
			{
				Rows = new List<KpirRow>()
				{
					new KpirRow{}
				}
			};

			var kpir2 = new Kpir
			{
				Rows = new List<KpirRow>()
				{
					new KpirRow{}
				}
			};

			_repository.GetAsync(guid1).Returns(Task.FromResult<Kpir?>(kpir1));
			_repository.GetAsync(guid2).Returns(Task.FromResult<Kpir?>(kpir2));

			// Act
			var result = await _sut.HandleAsync(command);

			// Assert
			result.Should().NotBeNull();
			result.AggregatedKpir.Should().NotBeNull();

			result.AggregatedKpir!.Id.Should().NotBe(guid1);
			result.AggregatedKpir.Id.Should().NotBe(guid2);
			result.AggregatedKpir.Id.Should().NotBe(Guid.Empty);

			result.AggregatedKpir.Header.Should().NotBeNull();
			result.AggregatedKpir.Header.DateFrom.Should().Be(new DateTime(1753, 01, 01)); // min SQL date
			result.AggregatedKpir.Header.DateTo.Should().Be(new DateTime(1753, 01, 01)); // min SQL date

			result.AggregatedKpir.PhysicalInventories.Should().BeEmpty();

			result.AggregatedKpir.ControlData.Should().NotBeNull();
			result.AggregatedKpir.Subject.Should().BeNull();

			result.AggregatedKpir.Summary.Should().NotBeNull();
			result.AggregatedKpir.Summary.PhysicalInventoryYearStart.Should().Be(0);
			result.AggregatedKpir.Summary.PhysicalInventoryYearEnd.Should().Be(0);

			result.AggregatedKpir.Rows.Should().HaveCount(2);
			result.AggregatedKpir.Rows.ElementAt(0).Number.Should().Be(1);
			result.AggregatedKpir.Rows.ElementAt(1).Number.Should().Be(2);

			result.SourceKpirs.Should().HaveCount(2);
			result.Warnings.Should().BeEmpty();
		}


		[Test]
		public async Task HandleAsync_ForSourcesWithPhysicalInventories_ShouldSucceeded()
		{
			// Arrange
			var guid1 = Guid.NewGuid();
			var guid2 = Guid.NewGuid();
			var command = new AggregateKpirCommand
			{
				Ids = [guid1, guid2]
			};

			var kpir1 = new Kpir
			{
				Summary = new KpirSummary
				{
					PhysicalInventoryYearStart = 2021,
					PhysicalInventoryYearEnd = 2022,
				},
				Rows = new List<KpirRow>()
				{
					new KpirRow{}
				},
				PhysicalInventories =
				[
					new KpirPhysicalInventory()
				],
				Header = new KpirHeader
				{
					DateFrom = new DateTime(2024, 01, 01),
					DateTo = new DateTime(2024, 01, 31)
				}
			};

			var kpir2 = new Kpir
			{
				Summary = new KpirSummary
				{
					PhysicalInventoryYearStart = 2022,
					PhysicalInventoryYearEnd = 2023,
				},
				Rows = new List<KpirRow>()
				{
					new KpirRow{}
				},
				PhysicalInventories =
				[
					new KpirPhysicalInventory()
				],
				Header = new KpirHeader
				{
					DateFrom = new DateTime(2024, 02, 01),
					DateTo = new DateTime(2024, 02, 28)
				}
			};

			_repository.GetAsync(guid1).Returns(Task.FromResult<Kpir?>(kpir1));
			_repository.GetAsync(guid2).Returns(Task.FromResult<Kpir?>(kpir2));

			// Act
			var result = await _sut.HandleAsync(command);

			// Assert
			result.Should().NotBeNull();
			result.AggregatedKpir.Should().NotBeNull();

			result.AggregatedKpir!.Id.Should().NotBe(guid1);
			result.AggregatedKpir.Id.Should().NotBe(guid2);
			result.AggregatedKpir.Id.Should().NotBe(Guid.Empty);

			result.AggregatedKpir.Header.Should().NotBeNull();
			result.AggregatedKpir.Header.DateFrom.Should().Be(new DateTime(2024, 01, 01));
			result.AggregatedKpir.Header.DateTo.Should().Be(new DateTime(2024, 02, 28));

			result.AggregatedKpir.PhysicalInventories.Should().HaveCount(2);

			result.AggregatedKpir.ControlData.Should().NotBeNull();
			result.AggregatedKpir.Subject.Should().BeNull();

			result.AggregatedKpir.Summary.Should().NotBeNull();
			result.AggregatedKpir.Summary.PhysicalInventoryYearStart.Should().Be(2021);
			result.AggregatedKpir.Summary.PhysicalInventoryYearEnd.Should().Be(2023);

			result.AggregatedKpir.Rows.Should().HaveCount(2);
			result.AggregatedKpir.Rows.ElementAt(0).Number.Should().Be(1);
			result.AggregatedKpir.Rows.ElementAt(1).Number.Should().Be(2);

			result.SourceKpirs.Should().HaveCount(2);
			result.Warnings.Should().BeEmpty();
		}
	}
}