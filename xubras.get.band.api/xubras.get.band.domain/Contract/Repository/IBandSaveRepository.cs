using xubras.get.band.domain.Domains;

namespace xubras.get.band.domain.Contract.Repository
{
    public interface IBandSaveRepository
    {
        Band CreateBand(Band band);
    }
}