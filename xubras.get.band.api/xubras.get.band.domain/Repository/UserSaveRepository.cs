using xubras.get.band.data.Entities;
using xubras.get.band.data.Persistence.EF;
using xubras.get.band.domain.Contract.Repository;
using xubras.get.band.domain.Repository.Base;

namespace xubras.get.band.domain.Repository
{
    public class UserSaveRepository : GenericSaveRepository<UserEntity>, IUserSaveRepository
    {
        public UserSaveRepository(GetBandContext context) : base() { }
    }
}