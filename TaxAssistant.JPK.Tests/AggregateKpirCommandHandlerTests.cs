using FluentAssertions;
using NSubstitute;
using TasAssistant.JPK.ApplicationLogic.CommandHandlers;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Tests
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
        public async Task HandleAsync_ForSourceWithNoRows_ShouldReturnWarning()
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
    }
}