using eSiafApiN4.DTOs.eSiafN4;
using FluentValidation;

namespace eSiafApiN4.Validaciones;

public class BancosDtoCreateValidator : AbstractValidator<BancosDtoCreate>
{
    public BancosDtoCreateValidator()
    {
        RuleFor(x => x.UidCia)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");
            
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Must(FnxCodigoIncorrecto).WithMessage("El campo {PropertyName} es incorrecto")
            .MaximumLength(10).WithMessage("La longitud máxima de caracteres permitidos para el campo {PropertyName} es {MaxLength}");

        RuleFor(x => x.Descripci)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");
    }

    private static bool FnxCodigoIncorrecto(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            return true;
        }
        valor = valor.Trim();
        return (!".-".Contains(valor));                    
    }
}