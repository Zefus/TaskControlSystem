using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data.Entity;

namespace TaskControlSystem.Infrastructure.Repository
{
    [Export(typeof(IRepository<>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        public DbSet<T> Set { get; }

        #region Sync Repository
        public EntityRepository(DbContext db)
        {
            _dbContext = db;
            Set = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Remove(entity);
        }

        public T Find(params object[] keyValues)
        {
            var dbSet = _dbContext.Set<T>();
            var entity = dbSet.Find(keyValues);
            return entity;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var dbSet = _dbContext.Set<T>().Where(predicate);
            return dbSet;
        }
        #endregion

        #region Async Repository
        public Task AddAsync(T entity)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Add(entity);
            return InternalHelper.CompletedTask;
        }

        public Task RemoveAsync(T entity)
        {
            Set.Remove(entity);
            return InternalHelper.CompletedTask;
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            T entity = await Set.FindAsync(keyValues).ConfigureAwait(false);
            return entity ?? throw new Exception("Entity is not found");
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var dbSet = _dbContext.Set<T>().Where(predicate);
            return await dbSet.ToListAsync();
        }
        #endregion
    }
}
