using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LNUbiz.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected LNUbizDBContext LNUbizDBContext { get; set; }

        public RepositoryBase(LNUbizDBContext _LNUbizDBContext)
        {
            this.LNUbizDBContext = _LNUbizDBContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.LNUbizDBContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.LNUbizDBContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.LNUbizDBContext.Set<T>().Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await this.LNUbizDBContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            this.LNUbizDBContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.LNUbizDBContext.Set<T>().Remove(entity);
        }

        public void Attach(T entity)
        {
            this.LNUbizDBContext.Set<T>().Attach(entity);
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IIncludableQueryable<T, object> query = null;

            if (includes.Length > 0)
            {
                query = this.LNUbizDBContext.Set<T>().Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? this.LNUbizDBContext.Set<T>() : (IQueryable<T>)query;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await this.GetQuery(predicate, include).ToListAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = this.GetQuery(predicate, include);
            return await query.FirstAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await this.GetQuery(predicate, include).FirstOrDefaultAsync();
        }

        public async Task<T> GetLastAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await this.GetQuery(predicate, include).LastAsync();
        }

        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await this.GetQuery(predicate, include).LastOrDefaultAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await this.GetQuery(predicate, include).SingleAsync();
        }

        public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await this.GetQuery(predicate, include).SingleOrDefaultAsync();
        }

        private IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = this.LNUbizDBContext.Set<T>().AsNoTracking();
            if (include != null)
            {
                query = include(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
    }
}