using xubras.get.band.data.Entities;
using xubras.get.band.data.Persistence.EF;
using xubras.get.band.domain.Contract.Repository;
using xubras.get.band.domain.Domains;
using xubras.get.band.domain.Repository.Base;

namespace xubras.get.band.domain.Repository
{
    public sealed class BandSaveRepository : GenericSaveRepository<BandEntity>, IBandSaveRepository
    {
        public BandSaveRepository(GetBandContext repositoryContext) : base()
        {
        }

        public Band CreateBand(Band band)
        {
            //base.Create(band);
            return band;
        }
    }
}