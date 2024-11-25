namespace TaxAssistant.CQRS.Abstraction
{
	public interface IGate
	{
		Task HandleAsync<T>(T command) where T : ICommand;

		Task<TOut> HandleAsync<TIn, TOut>(TIn command) where TIn : ICommand;
	}
}
