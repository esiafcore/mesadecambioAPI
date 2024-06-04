using eSiafApiN4.DTOs.XanesN8;
using FluentValidation;

namespace eSiafApiN4.Validaciones;

public class CredencialesUsuarioDtoValidator : AbstractValidator<CredencialesUsuarioDto>
{
    public CredencialesUsuarioDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(Utilidades.CampoRequeridoMensaje)
            .MaximumLength(256)
            .WithMessage(Utilidades.MaximumLengthMensaje)
            .EmailAddress()
            .WithMessage(Utilidades.EmailMensaje);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(Utilidades.CampoRequeridoMensaje);

    }
}