using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControlSystem.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> GetAll();

        Task AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
