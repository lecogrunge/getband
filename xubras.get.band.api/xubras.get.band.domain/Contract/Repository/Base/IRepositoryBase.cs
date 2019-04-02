using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace xubras.get.band.domain.Contract.Repository.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<IEnumerable<TEntity>> FindByConditionAync(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}