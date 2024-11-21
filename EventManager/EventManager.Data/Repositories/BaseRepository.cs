using EventManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        protected DbContext DbContext { get { return _context; } }

        public async virtual Task<bool> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            return await SaveChangesAsync();
        }

        public virtual Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> EditAsync(Guid id, TEntity newEntity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            int result = await _context.SaveChangesAsync();

            return result >= 1;
        }
    }
}
