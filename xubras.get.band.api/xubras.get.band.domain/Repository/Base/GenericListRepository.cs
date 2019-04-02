using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using xubras.get.band.data.Persistence.EF;
using xubras.get.band.domain.Contract.Repository.Base;

namespace xubras.get.band.domain.Repository.Base
{
    public class GenericListRepository<TEntity> : IGenericListRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public GenericListRepository(GetBandContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> where, List<string> includes = null)
        {
            var query = _dbSet.Where(where);

            if (includes != null)
                includes.ForEach(include => query = query.Include(include));

            return query.FirstOrDefaultAsync<TEntity>();
        }

        public Task<List<TEntity>> GetAll()
        {
            return _dbSet.ToListAsync<TEntity>();
        }

        public Task<TEntity> GetById(object[] id)
        {
            return _dbSet.FindAsync(id);
        }

        public Task<List<TEntity>> GetMany(Expression<Func<TEntity, bool>> where, List<string> includes = null)
        {
            var query = _dbSet.Where(where);

            if (includes != null)
                includes.ForEach(include => query = query.Include(include));

            return query.ToListAsync<TEntity>();
        }

        //public Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> where, List<string> includes = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<TEntity>> GetMany(Expression<Func<TEntity, bool>> where, List<string> includes = null)
        //{
        //    throw new NotImplementedException();
        //}
    }
}