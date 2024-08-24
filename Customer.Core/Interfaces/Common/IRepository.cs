using System.Linq.Expressions;

namespace Customer.Core.Interfaces.Common
{
    public interface IRepository<TEntity, TID> where TEntity : class
    {
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetByIdAsync(TID id);
        Task CreateAsync(TEntity entity);
        Task CreateManyAsync(List<TEntity> entities);

        void Update(TEntity entity);
        Task DeleteAsync(TID id);
        void DeleteWhere(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChangesAsync();
     

    }
}
