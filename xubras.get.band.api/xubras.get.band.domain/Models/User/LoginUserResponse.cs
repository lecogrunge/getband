using xubras.get.band.domain.Models.Base;

namespace xubras.get.band.domain.Models.User
{
    public sealed class LoginUserResponse : ResponseBase
    {
        public bool Authenticated { get; set; }
    }
}