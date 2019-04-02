namespace xubras.get.band.domain.Contract.Repository
{
    using System.Collections.Generic;
    using xubras.get.band.domain.Contract.Repository.Base;
    using xubras.get.band.domain.Entities;

    public interface IInstrumentalistListRepository : IGenericListRepository<InstrumentalistEntity>
    {
        List<InstrumentalistEntity> GetByName(string name);
    }
}
