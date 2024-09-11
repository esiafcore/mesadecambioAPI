using FluentValidation;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;

namespace XanesN8.Api.Validaciones;

public class ConfigCntDtoUpdateValidator : AbstractValidator<ConfigCntDtoUpdate>
{
    public ConfigCntDtoUpdateValidator(IRepositorioConfigCnt repo
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

    }
}