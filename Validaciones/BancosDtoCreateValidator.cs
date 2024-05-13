using eSiafApiN4.DTOs;
using FluentValidation;

namespace eSiafApiN4.Validaciones;

public class BancosDtoCreateValidator : AbstractValidator<BancosDtoCreate>
{
    public BancosDtoCreateValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .MaximumLength(10).WithMessage("La longitud máxima de caracteres permitidos para el campo {PropertyName} es {MaxLength}");

        RuleFor(x => x.Descripci)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");

    }
}