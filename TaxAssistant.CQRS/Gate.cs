using TaxAssistant.CQRS.Abstraction;

namespace TaxAssistant.CQRS
{
	public class Gate : IGate
    {
        private readonly IServiceProvider _serviceProvider;

        public Gate(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task HandleAsync<T>(T? command) where T : ICommand
        {
            var handlerType = typeof(ICommandHandler<T>);

            if (_serviceProvider.GetService(handlerType) is not ICommandHandler<T> handler)
            {
                throw new InvalidOperationException($"Handler for {typeof(T)} not found");
            }

            await handler.HandleAsync(command);
        }

        public async Task<TOut> HandleAsync<TIn, TOut>(TIn? command) where TIn : ICommand
        {
            var handlerType = typeof(ICommandHandler<TIn, TOut>);

            if (_serviceProvider.GetService(handlerType) is not ICommandHandler<TIn, TOut> handler)
            {
                throw new InvalidOperationException($"Handler for {typeof(TIn)}, {typeof(TOut)} not found");
            }

            return await handler.HandleAsync(command);
        }
    }
}
