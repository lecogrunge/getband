namespace xubras.get.band.domain.Contract.Repository.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGenericListRepository<TEntity> where TEntity : class
    {
        Task <TEntity> GetById(object[] id);
        Task <TEntity> GetFirst(Expression<Func<TEntity, bool>> where, List<string> includes = null);
        Task <List<TEntity>> GetAll();
        Task <List<TEntity>> GetMany(Expression<Func<TEntity, bool>> where, List<string> includes = null);
    }
}