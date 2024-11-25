using TaxAssistant.CQRS.Abstraction;

namespace TaxAssistant.JPK.Tests.CQRS.Model
{
	class ExampleCommandHandlerWithResult : ICommandHandler<ExampleCommand, ExampleCommandResult>
    {
        public Task<ExampleCommandResult> HandleAsync(ExampleCommand command) => Task.FromResult(new ExampleCommandResult());
    }
}