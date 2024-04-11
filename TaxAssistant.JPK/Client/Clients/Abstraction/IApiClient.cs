using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.View;

namespace TaxAssistant.JPK.Client.Clients.Abstraction
{
    public interface IApiClient<T> where T : BaseModel
    {
        Task<IList<T>> GetAsync();

        Task<IList<Selectable<T>>> GetSelectableAsync();

        Task<T> GetAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}
