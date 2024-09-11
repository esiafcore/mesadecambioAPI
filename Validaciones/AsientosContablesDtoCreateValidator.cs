using FluentValidation;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;

namespace XanesN8.Api.Validaciones;

public class AsientosContablesDtoCreateValidator : AbstractValidator<AsientosContablesDtoCreate>
{
    public AsientosContablesDtoCreateValidator(IRepositorioAsientoContable repo)
    {
        RuleFor(x => x.UidCia)
            .NotEmpty().WithMessage(Utilidades.CampoRequeridoMensaje);
    }
}