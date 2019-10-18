using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskControlSystem.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
