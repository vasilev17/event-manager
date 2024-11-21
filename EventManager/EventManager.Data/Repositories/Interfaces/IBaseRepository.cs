namespace EventManager.Data.Repositories.Interfaces
{
    internal interface IBaseRepository<TEntity> where TEntity : class
    {
        //Add Entity to database
        Task<bool> AddAsync(TEntity entity);

        //Find entity by id
        Task<TEntity> GetByIdAsync(Guid id);

        //Modify Entity from database
        Task<bool> EditAsync(Guid id, TEntity newEntity);

        //Delete Entity from database
        Task<bool> DeleteAsync(TEntity entity);
    }
}
