using TaxAssistant.JPK.Shared.Model.Abstraction;
using TaxAssistant.JPK.Shared.Model.View;

namespace TaxAssistant.JPK.Client.Clients.Abstraction
{
	public interface IApiClient<T> where T : BaseModel
    {
        Task<IList<T>?> GetAsync();

        Task<T?> GetAsync(Guid id, string? action = null);

        Task<IList<Selectable<T>>?> GetSelectableAsync();

        Task DeleteAsync(Guid id);
        
        Task<T?> UpdateAsync(T entity);
    }
}
