namespace xubras.get.band.domain.Contract.Repository.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IGenericSaveRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddAll(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateAll(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);
        void SaveChangesAsync();
    }
}