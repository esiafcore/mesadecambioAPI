using FluentValidation;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;

namespace XanesN8.Api.Validaciones;

public class ConsecutivosBcoDetalleDtoUpdateValidator : AbstractValidator<ConsecutivosBcoDetalleDtoUpdate>
{
    public ConsecutivosBcoDetalleDtoUpdateValidator(IRepositorioConsecutivosBcoDetalle repo
        , IHttpContextAccessor httpContextAccessor)
    {
        var valorRutaId = httpContextAccessor.HttpContext?.Request.RouteValues["id"];
        Guid id = Guid.Empty;

        if (valorRutaId is string valorString)
        {
            Guid.TryParse(valorString, out id);
        }

        RuleFor(x => x.UidCia)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");

        //RuleFor(x => x.Codigo)
        //    .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
        //    .Must(FnxCodigoIncorrecto).WithMessage("El campo {PropertyName} es incorrecto")
        //    .MaximumLength(10).WithMessage("La longitud máxima de caracteres permitidos para el campo {PropertyName} es {MaxLength}")
        //    .MustAsync(async (codigo, _) =>
        //    {
        //        var existe = await repo.Exist(id, code: codigo);
        //        return !existe;
        //    }).WithMessage(x => $"Ya existe un Consecutivo con el código {x.Codigo}");


        RuleFor(x => x.NombreCampo)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");
    }

    private static bool FnxCodigoIncorrecto(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            return true;
        }
        valor = valor.Trim();
        return !".-".Contains(valor);
    }
}