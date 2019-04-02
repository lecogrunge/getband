namespace xubras.get.band.domain.Repository
{
    using System;
    using System.Collections.Generic;
    using xubras.get.band.data.Persistence.EF;
    using xubras.get.band.domain.Contract.Repository;
    using xubras.get.band.domain.Entities;
    using xubras.get.band.domain.Repository.Base;

    public class FaListRepository : GenericListRepository<FaEntity>, IFaListRepository
    {
        public FaListRepository(GetBandContext getBandContext) : base(getBandContext) { }

        public List<FaEntity> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}