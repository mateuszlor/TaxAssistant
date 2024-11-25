using System.Linq.Expressions;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction
{
	public interface IRepository<T>
		where T : BaseModel
	{
		Task<T> AddAsync(T item);

		Task<T> UpdateAsync(T item);

		Task<IList<T>> GetAllAsync();

		Task<T?> GetAsync(Guid id);

		Task DeleteAsync(Guid id);

		Task<bool> AnyAsync(Expression<Func<T, bool>> query);
	}
}
