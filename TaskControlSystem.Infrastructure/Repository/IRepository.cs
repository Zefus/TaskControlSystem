using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControlSystem.Infrastructure.Repository
{
    public interface IRepository<TItem> where TItem : class
    {
        void Add(TItem item);
        void Remove(TItem item);
        TItem Find(int id);
        IQueryable<TItem> GetAll();
    }
}
