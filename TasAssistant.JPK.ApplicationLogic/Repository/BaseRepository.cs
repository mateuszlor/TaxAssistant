using Microsoft.EntityFrameworkCore;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
    public abstract class BaseRepository<T>
        where T: BaseModel
    {
        protected readonly DatabaseContext _databaseContext;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public virtual async Task<T> AddAsync(T item)
        {
            item.Version = 1;
            item.ModificationDate = null;
            item.CreationDate = DateTime.UtcNow;

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

            item.ModificationDate = DateTime.UtcNow;
            item.CreationDate = existingItem.CreationDate;
            item.Version = existingItem.Version + 1;

            var newItem = _databaseContext.Update(item);
            await _databaseContext.SaveChangesAsync();

            return newItem.Entity;
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _databaseContext.Set<T>().ToListAsync();
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
            var item = await GetAsync(id);

            if(item != null)
            {
                item.IsDeleted = true;
            }
        }
    }
}
