using FluentValidation;

namespace xubras.get.band.domain.Domains.Validation
{
    public sealed class UserValidator : AbstractValidator<User.User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome obrigatório.");
            RuleFor(x => x.NickName).NotEmpty().WithMessage("Nome de Usuário obrigatório.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Senha obrigatório.");
        }
    }
}