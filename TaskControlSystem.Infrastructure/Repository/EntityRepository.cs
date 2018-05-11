using System;
using System.Collections.Generic;
using System.Linq;
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

        public EntityRepository(DbContext db)
        {
            _dbContext = db;
        }

        public void Add(T item)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Add(item);
        }

        public void Remove(T item)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Remove(item);
        }

        public T Find(int id)
        {
            var dbSet = _dbContext.Set<T>();
            var item = dbSet.Find(id);
            return item;
        }

        public IQueryable<T> GetAll()
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet;
        }
    }
}
