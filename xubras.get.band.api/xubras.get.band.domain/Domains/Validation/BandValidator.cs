using FluentValidation;
using xubras.get.band.domain.Domains.User;

namespace xubras.get.band.domain.Domains.Validation
{
    public sealed class BandValidator : AbstractValidator<Band>
    {
        public BandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome obrigatório.");
        }
    }
}