using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using xubras.get.band.domain.Contract.Repository.Base;

namespace xubras.get.band.domain.Repository.Base
{
    public class GenericSaveRepository<TEntity> : IGenericSaveRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void AddAll(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}