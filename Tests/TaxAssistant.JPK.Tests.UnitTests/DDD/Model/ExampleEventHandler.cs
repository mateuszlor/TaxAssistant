using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Tests.CQRS.Model
{
    class ExampleEventHandler : IDomainEventHandler<ExampleEvent>
    {
        public async Task HandleAsync(ExampleEvent? domanEvent) => await Task.CompletedTask;
    }
}