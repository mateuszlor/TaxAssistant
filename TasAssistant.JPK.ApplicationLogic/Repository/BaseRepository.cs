using Microsoft.EntityFrameworkCore;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
	public abstract class BaseRepository<T> : IRepository<T>
        where T : BaseModel
    {
        protected readonly DatabaseContext _databaseContext;

        protected BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public virtual async Task<T> AddAsync(T item)
        {
            var result = _databaseContext.Add(item);

            await _databaseContext.SaveChangesAsync();

            return result.Entity;
        }

        public virtual async Task<T> UpdateAsync(T item)
        {
            var existingItem = await GetAsync(item.Id);

            if (existingItem == null)
            {
                throw new InvalidOperationException($"{typeof(T).Name} with Id='{item.Id}' not exist");
            }

			item.IncrementVersion(existingItem);

            var newItem = _databaseContext.Update(item);
            await _databaseContext.SaveChangesAsync();

            return newItem.Entity;
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _databaseContext
                .Set<T>()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public virtual async Task<T?> GetAsync(Guid id)
        {
            var set = _databaseContext.Set<T>();

            if (set == null)
            {
                return null;
            }

            var result = await set.SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var existingItem = await GetAsync(id);

            if (existingItem != null && !existingItem.IsDeleted)
            {
                existingItem.Delete();
                await UpdateAsync(existingItem);
            }
        }
    }
}
