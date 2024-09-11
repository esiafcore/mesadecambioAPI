using FluentValidation;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;

namespace XanesN8.Api.Validaciones;

public class ConsecutivosCntDtoUpdateValidator : AbstractValidator<ConsecutivosCntDtoUpdate>
{
    public ConsecutivosCntDtoUpdateValidator(IRepositorioConsecutivoCnt repo
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