namespace TaxAssistant.CQRS
{
    public class Gate
    {
        private readonly IServiceProvider _serviceProvider;

        public Gate(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task HandleAsync<T>(T command) where T : ICommand
        {
            var handlerType = typeof(ICommandHandler<T>);
            var handler = _serviceProvider.GetService(handlerType) as ICommandHandler<T>;

            if (handler == null)
            {
                throw new Exception($"Handler for {typeof(T)} not found");
            }

            await handler.HandleAsync(command);
        }

        public async Task<TOut> HandleAsync<TIn, TOut>(TIn command) where TIn : ICommand
        {
            var handlerType = typeof(ICommandHandler<TIn, TOut>);
            var handler = _serviceProvider.GetService(handlerType) as ICommandHandler<TIn, TOut>;

            if (handler == null)
            {
                throw new Exception($"Handler for {typeof(TIn)}, {typeof(TOut)} not found");
            }

            return await handler.HandleAsync(command);
        }
    }
}
