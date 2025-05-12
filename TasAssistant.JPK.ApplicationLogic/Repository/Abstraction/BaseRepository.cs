using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : BaseModel, IAggregate
    {
        protected readonly DatabaseContext _databaseContext;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ILogger<BaseRepository<T>> _logger;

        protected BaseRepository(DatabaseContext databaseContext, IDomainEventDispatcher dispatcher, ILogger<BaseRepository<T>> logger)
        {
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<T> AddAsync(T item)
        {
            item.CreationDate = DateTime.UtcNow;
            item.Version = 1;

            var result = _databaseContext.Add(item);

            await HandleEvents(item);

            await _databaseContext.SaveChangesAsync();

            return result.Entity;
        }

        public virtual async Task<T> UpdateAsync(T item)
        {
            var existingItem = await GetAsync(item.Id, true);

            if (existingItem == null)
            {
                throw new InvalidOperationException($"{typeof(T).Name} with Id='{item.Id}' not exist");
            }

            var attachedItem = _databaseContext.Attach(item);

            attachedItem.Entity.IncrementVersion(existingItem);
            attachedItem.State = EntityState.Modified;

            var newItem = _databaseContext.Update(attachedItem.Entity);

            await HandleEvents(item);

            try
            {
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity: {ErrorMessage}", ex.InnerException?.Message ?? ex.Message);
                throw;
            }

            return newItem.Entity;
        }

        public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            var query = _databaseContext
                .Set<T>()
                .Where(x => !x.IsDeleted);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query
                .OrderBy(x => x.CreationDate)
                .ToListAsync();
        }

        public virtual async Task<T?> GetAsync(Guid id, bool noTracking = false)
        {
            var set = _databaseContext
                .Set<T>()
                .AsQueryable<T>();

            if (noTracking)
            {
                set = set.AsNoTracking();
            }

            var result = await set.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

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

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> query)
        {
            return await _databaseContext
                .Set<T>()
                .Where(x => !x.IsDeleted)
                .Where(query)
                .AnyAsync();
        }

        private async Task HandleEvents(T item)
        {
            foreach (var e in item.Events)
            {
                try
                {
                    await _dispatcher.DispatchAsync(e);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling domain event {EventType}", e.GetType().Name);
                    throw new InvalidOperationException("Error handling domain event", ex);
                }
            }

            item.Events.Clear();
        }
    }
}
