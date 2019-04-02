namespace xubras.get.band.domain.Models.User
{
    public sealed class CreateUserRequest
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}