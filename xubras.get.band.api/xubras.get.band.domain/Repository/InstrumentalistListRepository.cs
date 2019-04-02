namespace xubras.get.band.domain.Repository
{
    using System;
    using System.Collections.Generic;
    using xubras.get.band.data.Persistence.EF;
    using xubras.get.band.domain.Contract.Repository;
    using xubras.get.band.domain.Entities;
    using xubras.get.band.domain.Repository.Base;

    public class InstrumentalistListRepository : GenericListRepository<InstrumentalistEntity>, IInstrumentalistListRepository
    {
        public InstrumentalistListRepository(GetBandContext getBandContext) : base(getBandContext) { }

        public List<InstrumentalistEntity> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
