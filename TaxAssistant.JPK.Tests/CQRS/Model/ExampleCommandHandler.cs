using TaxAssistant.CQRS;

namespace TaxAssistant.JPK.Tests.CQRS.Model
{
    class ExampleCommandHandler : ICommandHandler<ExampleCommand>
    {
        public Task HandleAsync(ExampleCommand? command) => Task.CompletedTask;
    }
}