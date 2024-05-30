using eSiafApiN4.DTOs.eSiafN4;
using eSiafApiN4.Repositorios.eSiafN4;
using FluentValidation;

namespace eSiafApiN4.Validaciones;

public class BancosDtoCreateValidator : AbstractValidator<BancosDtoCreate>
{
    public BancosDtoCreateValidator(IRepositorioBanco repo)
    {
        RuleFor(x => x.UidCia)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");
            
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Must(FnxCodigoIncorrecto).WithMessage("El campo {PropertyName} es incorrecto")
            .MaximumLength(10).WithMessage("La longitud máxima de caracteres permitidos para el campo {PropertyName} es {MaxLength}")
            .MustAsync(async (codigo, _) =>
            {
                var existe = await repo.Exist(id: Guid.Empty, code: codigo);
                return !existe;
            }).WithMessage(x => $"Ya existe un Banco con el código {x.Codigo}");

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