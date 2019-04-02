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
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected GetBandContext _context { get; set; }

        public RepositoryBase(GetBandContext repositoryContext)
        {
            this._context = repositoryContext;
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await this._context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAync(Expression<Func<TEntity, bool>> expression)
        {
            return await this._context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public void Create(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            this._context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            this._context.Set<TEntity>().Remove(entity);
        }

        Task<IEnumerable<TEntity>> IRepositoryBase<TEntity>.FindAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}