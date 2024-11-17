using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
	public interface IRepository<T>
        where T : BaseModel
    {
        Task<T> AddAsync(T item);

        Task<T> UpdateAsync(T item);

        Task<IList<T>> GetAllAsync();

        Task<T?> GetAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}
