using FluentValidation;

namespace xubras.get.band.domain.ValueObjects.Validation
{
    public sealed class EmailVallidator : AbstractValidator<Email>
    {
        public EmailVallidator()
        {
            RuleFor(x => x.EmailAddress).Cascade(CascadeMode.StopOnFirstFailure)
                                        .NotEmpty().WithMessage("E-mail obrigatório.")
                                        .EmailAddress().WithMessage("E-mail inválido.");
        }
    }
}