using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modern.NW.Persistence
{
    public interface IPersistence<T>
    {
        Task Insert(T entity, bool commit);
        Task Update(T entity, bool commit);
        Task Delete(T entity, bool commit);
        Task Commit();
        IQueryable<T> SearchBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
    }
}
