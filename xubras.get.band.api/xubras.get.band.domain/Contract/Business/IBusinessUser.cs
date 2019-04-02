using xubras.get.band.domain.Models.User;

namespace xubras.get.band.domain.Contract.Business
{
    public interface IBusinessUser
    {
        CreateUserResponse CreateUser(CreateUserRequest request);
        LoginUserResponse Login(LoginUserRequest request);
    }
}