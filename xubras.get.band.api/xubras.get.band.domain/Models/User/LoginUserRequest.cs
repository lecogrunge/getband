using xubras.get.band.domain.Enums;

namespace xubras.get.band.domain.Models.User
{
    public sealed class LoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}