﻿using FluentValidation;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;

namespace XanesN8.Api.Validaciones;

public class BancosDtoCreateValidator : AbstractValidator<BancosDtoCreate>
{
    public BancosDtoCreateValidator(IRepositorioBanco repo)
    {
        RuleFor(x => x.UidCia)
            .NotEmpty().WithMessage(Utilidades.CampoRequeridoMensaje);

        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage(Utilidades.CampoRequeridoMensaje)
            .Must(FnxCodigoIncorrecto).WithMessage(Utilidades.ValorIncorrectoMensaje)
            .MaximumLength(10).WithMessage(Utilidades.MaximumLengthMensaje)
            .MustAsync(async (codigo, _) =>
            {
                var existe = await repo.Exist(id: Guid.Empty, code: codigo);
                return !existe;
            }).WithMessage(x => $"Ya existe un Banco con el código {x.Codigo}");

        RuleFor(x => x.Descripci)
            .NotEmpty().WithMessage(Utilidades.CampoRequeridoMensaje);
    }

    private static bool FnxCodigoIncorrecto(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            return true;
        }
        valor = valor.Trim();
        return !Utilidades.CaracteresInvalidos.Contains(valor);
    }
}