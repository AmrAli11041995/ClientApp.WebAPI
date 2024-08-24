using Customer.Core.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Customer.Infrastructure.Repository
{
    public class Repository<TEntity, TID, TContext> : IRepository<TEntity, TID>
       where TEntity : class
       where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public static IDbContextTransaction _trans;
        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? _dbSet.Where(predicate) : _dbSet;
        }
        public async Task<TEntity> GetByIdAsync(TID id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public virtual async Task CreateManyAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
        public virtual async Task DeleteAsync(TID id)
        {
            TEntity entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
        }
        public virtual void DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> entities = GetWhere(predicate);
            _dbSet.RemoveRange(entities);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
