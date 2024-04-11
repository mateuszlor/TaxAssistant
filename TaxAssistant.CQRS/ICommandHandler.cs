namespace TaxAssistant.CQRS
{
    public interface ICommandHandler<TIn>
        where TIn : ICommand
    {
        Task HandleAsync(TIn command);
    }

    public interface ICommandHandler<TIn, TOut>
        where TIn : ICommand
    {
        Task<TOut> HandleAsync(TIn command);
    }
}
