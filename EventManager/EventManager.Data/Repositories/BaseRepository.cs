using EventManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected ApplicationDbContext DbContext { get { return _context; } }

        public async virtual Task<bool> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            return await SaveChangesAsync();
        }

        public async virtual Task<bool> DeleteAsync(TEntity entity)
        {
            this._context.Remove(entity);

            return await this.SaveChangesAsync();
        }

        public async virtual Task<bool> EditAsync(Guid id, TEntity newEntity)
        {
            var entry = this._context.Entry(newEntity);
            if (entry.State == EntityState.Detached)
                this._context.Attach(newEntity);

            entry.State = EntityState.Modified;

            return await this.SaveChangesAsync();
        }

        public async virtual Task<TEntity> GetByIdAsync(Guid id)
        {
            return await this._context
                .Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            int result = await _context.SaveChangesAsync();

            return result >= 1;
        }
    }
}
