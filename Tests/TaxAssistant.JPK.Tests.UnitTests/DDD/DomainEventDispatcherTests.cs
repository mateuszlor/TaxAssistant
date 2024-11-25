using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TaxAssistant.DDD;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.Tests.CQRS.Model;

namespace TaxAssistant.JPK.Tests.UnitTests.DDD
{
    public class DomainEventDispatcherTests
    {
        private IServiceProvider _serviceProvider;
        private DomainEventDispatcher _sut;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();

            _sut = new DomainEventDispatcher(_serviceProvider);
        }

        [Test]
        public async Task DispatchAsync_ForNoHandler_ShouldSucceed()
        {
            // Arrange 
            _serviceProvider
                .GetService<IEnumerable<IDomainEventHandler<ExampleEvent>>>()
                .Returns([]);

            // Act
            await _sut.DispatchAsync(new ExampleEvent());
        }

        [Test]
        public async Task DispatchAsync_ForRegisteredhandler_ShouldSucceed()
        {
            // Arrange 
            _serviceProvider
                .GetService<IEnumerable<IDomainEventHandler<ExampleEvent>>>()
                .Returns([new ExampleEventHandler()]);

            _serviceProvider
                .GetService<IDomainEventHandler<ExampleEvent>>()
                .Returns(new ExampleEventHandler());

            // Act
            await _sut.DispatchAsync(new ExampleEvent());
        }

        [Test]
        public async Task DispatchAllAsync_ForRegisteredhandler_ShouldSucceed()
        {
            // Arrange 
            _serviceProvider
                .GetService<IEnumerable<IDomainEventHandler<ExampleEvent>>>()
                .Returns([new ExampleEventHandler()]);

            _serviceProvider
                .GetService<IDomainEventHandler<ExampleEvent>>()
                .Returns(new ExampleEventHandler());

            // Act
            await _sut.DispatchAllAsync([
                new ExampleEvent(),
                new ExampleEvent(),
                new ExampleEvent()
                ]);
        }
    }
}