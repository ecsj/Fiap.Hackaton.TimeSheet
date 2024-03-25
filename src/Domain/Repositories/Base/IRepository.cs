namespace Domain.Repositories.Base;
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    IQueryable<TEntity> Get();
    IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
    Task<TEntity> GetByIdAsync<TId>(TId id) where TId : notnull;
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}