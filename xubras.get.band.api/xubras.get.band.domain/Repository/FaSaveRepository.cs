namespace xubras.get.band.domain.Repository
{
    using xubras.get.band.data.Persistence.EF;
    using xubras.get.band.domain.Contract.Repository;
    using xubras.get.band.domain.Entities;
    using xubras.get.band.domain.Repository.Base;

    public class FaSaveRepository : GenericSaveRepository<FaEntity>, IFaSaveRepository
    {
        public FaSaveRepository(GetBandContext GetBandContext) : base() { }
    }
}