namespace TaxAssistant.CQRS.Abstraction
{
	public interface ICommandHandler<in TIn>
		where TIn : ICommand
	{
		Task HandleAsync(TIn? command);
	}

	public interface ICommandHandler<TIn, TOut>
		where TIn : ICommand
	{
		Task<TOut> HandleAsync(TIn? command);
	}
}
