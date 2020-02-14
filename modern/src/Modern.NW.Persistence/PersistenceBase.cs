using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modern.NW.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Modern.NW.Persistence
{
    public class PersistenceBase<T> : IPersistence<T> where T : class
    {
        protected String _entitySetName = String.Empty;
        private ILogger<PersistenceBase<T>> logger { get; set; }

        public NorthwindContext DataContext { get; set; }

        public PersistenceBase(NorthwindContext dataContext, ILogger<PersistenceBase<T>> logger)
        {
            DataContext = dataContext;
            this.logger = logger;
        }

        #region IPersistence<T> Members

        public virtual async Task Insert(T entity, bool commit)
        {
            await InsertObjectAsync(entity, commit);
        }

        public virtual async Task Update(T entity, bool commit)
        {
            await UpdateObjectAsync(entity, commit);
        }

        public virtual async Task Delete(T entity, bool commit)
        {
            await DeleteObjectAsync(entity, commit);
        }

        public virtual async Task Commit()
        {
            await SaveChangesAsync();
        }

        public virtual IQueryable<T> SearchBy(Expression<Func<T, bool>> predicate)
        {
            return EntitySet.Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return EntitySet;
        }
        #endregion

        protected virtual Task<T> FindMatchedOneAsync(T toBeMatched)
        {
            throw new ApplicationException("PersistenceBase.EntitySet: Shouldn't get here.");
        }

        protected virtual IQueryable<T> EntitySet
        {
            get { throw new ApplicationException("PersistenceBase.EntitySet: Shouldn't get here."); }
        }

        protected async Task InsertObjectAsync(T entity, bool commit)
        {
            DataContext.Entry(entity).State = EntityState.Added;
            try
            {
                if (commit) await SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }

        protected async Task UpdateObjectAsync(T entity, bool commit)
        {
            try
            {
                var entry = DataContext.Entry(entity);
                DataContext.Entry(entity).State = EntityState.Modified;
                if (commit) await SaveChangesAsync();
            }
            catch (InvalidOperationException e) // Usually the error getting here will have a message: "an object with the same key already exists in the ObjectStateManager. The ObjectStateManager cannot track multiple objects with the same key"
            {
                T t = await FindMatchedOneAsync(entity);
                if (t == null) throw new ApplicationException("Entity doesn't exist in the repository");
                try
                {
                    DataContext.Entry(t).State = EntityState.Detached;
                    (EntitySet as DbSet<T>).Attach(entity);
                    DataContext.Entry(entity).State = EntityState.Modified;
                    if (commit) await SaveChangesAsync();
                }
                catch (Exception exx)
                {
                    //Roll back
                    DataContext.Entry(entity).State = EntityState.Detached;
                    (EntitySet as DbSet<T>).Attach(t);
                    logger.LogError(exx, exx.Message);
                    throw;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }

        protected async Task DeleteObjectAsync(T entity, bool commit)
        {
            T t = await FindMatchedOneAsync(entity);
            (EntitySet as DbSet<T>).Remove(t);
            try
            {
                if (commit) await SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }

        protected async Task SaveChangesAsync()
        {
            try
            {
                await DataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update original values from the database (Similar ClientWins in ObjectContext.Refresh)
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                await DataContext.SaveChangesAsync();
            }
        } // End of function
    }
}
