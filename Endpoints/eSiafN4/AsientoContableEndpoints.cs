using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Servicios;
using FluentValidation;
using Microsoft.AspNetCore.OutputCaching;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.Filtros;
using static XanesN8.Api.Utilidades.Enumeradores;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class AsientoContableEndpoints
{
    public static RouteGroupBuilder MapAsientoContable(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagAsientosContables))
            .RequireAuthorization();

        //group.MapGet("/{id:Guid}", GetById)
        //    .RequireAuthorization(AC.IsAdminClaim, AC.IsPowerUserClaim
        //    , AC.IsOperatorClaim);

        group.MapGet("/{id:Guid}", GetById)
            .RequireAuthorization();

        group.MapGet("getnextsecuentialnumber/", GetNextSecuentialNumber)
            .RequireAuthorization();

        group.MapPost("/", Create)
            .DisableAntiforgery()
            .AddEndpointFilter<FiltroValidaciones<AsientosContablesDtoCreate>>()
            .RequireAuthorization();

        group.MapPut("/{id:Guid}", Update)
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapDelete("/{id:Guid}", Delete)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Results<Ok<List<AsientosContablesDto>>, BadRequest<string>>> GetAlls(Guid companyId, int yearfiscal, int mesfiscal
        , IRepositorioAsientoContable repo
        , IMapper mapper
        , IServicioUsuarios srvUser
        , int pagina = 1, int recordsPorPagina = 10)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            YearMonthParams queryParams = new()
            {
                Uidcia = companyId,
                Yearfiscal = yearfiscal,
                Mesfiscal = mesfiscal,
                Pagina = pagina,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repo.GetAlls(queryParams);
            var objList = mapper.Map<List<AsientosContablesDto>>(dataList);

            return TypedResults.Ok(objList);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<AsientosContablesDto>, NotFound<string>, BadRequest<string>>> GetById(Guid id
        , IRepositorioAsientoContable repo
        , IMapper mapper
        , IServicioUsuarios srvUser)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            var dataItem = await repo.GetById(id);
            if (dataItem is null)
            {
                return TypedResults.NotFound("Asiento contable no encontrado");
            }

            var objItem = mapper.Map<AsientosContablesDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<string>, NotFound<string>, BadRequest<string>>> GetNextSecuentialNumber(
       Guid companyId,
       Guid bankAccountId,
       int fiscalYear,
       int fiscalMonth,
       short tipo,
       short subtipo,
       IRepositorioAsientoContable repo,
       IRepositorioConfigCnt repoConfigCnt,
       IRepositorioConsecutivoCnt repoConsecutivoCnt,
       IRepositorioConsecutivoCntDetalle repoConsecutivoCntDetalle,
       IRepositorioCuentaBancaria repoCuentaBancaria,
       IMapper mapper,
       IServicioUsuarios srvUser,
       ConsecutivoTipo consecutivo = ConsecutivoTipo.Temporal,
       bool isSave = false)

    {
        try
        {

            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            int numberTransa = 0;
            string numberFull = string.Empty;
            CuentasBancarias? cuentaModel = new();
            ConsecutivosCnt? consecutivoCntModel = new();
            ConsecutivosCntDetalle? consecutivoCntDetalleModel = new();
            List<ConsecutivosCnt>? consecutivoCntList = new();
            List<ConsecutivosCntDetalle>? consecutivoCntDetalleList = new();

            if (companyId == Guid.Empty)
            {
                return TypedResults.BadRequest("Compañia es requerida");
            }

            QueryParams queryParams = new QueryParams
            {
                Uidcia = companyId,
                Pagina = 1,
                RecordsPorPagina = 0
            };

            YearMonthParams yearMonthParams = new YearMonthParams
            {
                Uidcia = companyId,
                Yearfiscal = fiscalYear,
                Mesfiscal = fiscalMonth,
                Pagina = 1,
                RecordsPorPagina = 0
            };

            var configCnt = await repoConfigCnt.GetByCia(companyId);
            if (configCnt is null)
            {
                return TypedResults.NotFound("Configuración de banco no encontrada");
            }

            var transaTipo = (TransaccionBcoTipo)tipo;

            //Si es pago, sacar el consecutivo de la cuenta
            if (transaTipo == TransaccionBcoTipo.Pago &&
                (TransaccionBcoPagoSubtipo)subtipo == TransaccionBcoPagoSubtipo.MesaCambio)
            {
                cuentaModel = await repoCuentaBancaria.GetById(bankAccountId);

                if (cuentaModel is null)
                {
                    return TypedResults.NotFound("Cuenta bancaria no encontrada");
                }

                switch (consecutivo)
                {
                    case ConsecutivoTipo.Temporal:
                        cuentaModel.ContadorTemporalExchange++;
                        numberTransa = (int)cuentaModel.ContadorTemporalExchange;
                        await repoCuentaBancaria.Update(mapper.Map<CuentasBancariasDtoUpdate>(cuentaModel));

                        break;
                    case ConsecutivoTipo.Perpetuo:
                        cuentaModel.ContadorExchange++;
                        numberTransa = (int)cuentaModel.ContadorExchange;

                        if (isSave)
                            await repoCuentaBancaria.Update(mapper.Map<CuentasBancariasDtoUpdate>(cuentaModel));

                        break;
                }
            }
            else
            {
                var consecutivoTipo =
                    (ContabilidadConsecutivoPor.Tipo | ContabilidadConsecutivoPor.TipoMensual | ContabilidadConsecutivoPor.TipoAnual);

                var consecutivoPor = (ContabilidadConsecutivoPor)configCnt.ConsecutivoAsientopor;

                //Si los consecutivos son por tipo
                if ((consecutivoPor & consecutivoTipo) == consecutivoPor)
                {
                    consecutivoCntDetalleList = await repoConsecutivoCntDetalle.GetAlls(yearMonthParams);

                    if (consecutivoCntDetalleList is null || consecutivoCntDetalleList.Count == 0)
                    {
                        return TypedResults.NotFound("Detalles de consecutivos no encontrados");
                    }

                    consecutivoCntDetalleModel = consecutivoCntDetalleList
                        .FirstOrDefault(x => x.Categoria.Trim() == AC.CategoryBcoByDefault &&
                                             x.Codigo.Trim() == AC.CategoryBcoByDefault);

                    if (consecutivoCntDetalleModel is null)
                    {
                        return TypedResults.NotFound($"Detalle de consecutivo: {AC.CategoryBcoByDefault}-{AC.CategoryBcoByDefault} no encontrado");
                    }

                    switch (consecutivo)
                    {
                        case ConsecutivoTipo.Temporal:
                            consecutivoCntDetalleModel.ContadorTemporal++;
                            numberTransa = (int)consecutivoCntDetalleModel.ContadorTemporal;
                            await repoConsecutivoCntDetalle.Update(mapper.Map<ConsecutivosCntDetalleDtoUpdate>(consecutivoCntDetalleModel));

                            break;
                        case ConsecutivoTipo.Perpetuo:
                            consecutivoCntDetalleModel.Contador++;
                            numberTransa = (int)consecutivoCntDetalleModel.Contador;

                            if (isSave)
                                await repoConsecutivoCntDetalle.Update(mapper.Map<ConsecutivosCntDetalleDtoUpdate>(consecutivoCntDetalleModel));

                            break;
                    }

                }
                else
                {
                    consecutivoCntList = await repoConsecutivoCnt.GetAlls(queryParams);

                    if (consecutivoCntList is null || consecutivoCntList.Count == 0)
                    {
                        return TypedResults.NotFound("Consecutivos no encontrados");
                    }

                    consecutivoCntModel = consecutivoCntList
                        .FirstOrDefault(x => x.Categoria == AC.CategoryCntByDefault &&
                                             x.Codigo == AC.CategoryCntByDefault);

                    if (consecutivoCntModel is null)
                    {
                        return TypedResults.NotFound("Consecutivo no encontrado");
                    }

                    switch (consecutivo)
                    {
                        case ConsecutivoTipo.Temporal:
                            consecutivoCntModel.ContadorTemporal++;
                            numberTransa = (int)consecutivoCntModel.ContadorTemporal;
                            await repoConsecutivoCnt.Update(mapper.Map<ConsecutivosCntDtoUpdate>(consecutivoCntModel));

                            break;
                        case ConsecutivoTipo.Perpetuo:
                            consecutivoCntModel.Contador++;
                            numberTransa = (int)consecutivoCntModel.Contador;

                            if (isSave)
                                await repoConsecutivoCnt.Update(mapper.Map<ConsecutivosCntDtoUpdate>(consecutivoCntModel));

                            break;
                    }
                }
            }

            return TypedResults.Ok(numberTransa.ToString()
                .PadLeft(AC.TransactionTotalDigitsNumberDefault, AC.CharDefaultEmpty));
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Created<AsientosContablesDto>, NotFound<string>, BadRequest<string>
      , ValidationProblem>>
      Create(AsientosContablesDtoCreate modelDtoCreate
      , IRepositorioAsientoContable repo, IOutputCacheStore outputCacheStore
      , IMapper mapper, IServicioUsuarios srvUser, IValidator<AsientosContablesDtoCreate> validator)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            var resultadoValidacion = await validator.ValidateAsync(modelDtoCreate);
            if (!resultadoValidacion.IsValid)
            {
                return TypedResults.ValidationProblem(resultadoValidacion.ToDictionary());
            }


            var uid = await repo.Create(modelDtoCreate);

            var dataItem = await repo.GetById(uid);

            if (dataItem is null)
            {
                return TypedResults.NotFound("Asiento contable no encontrado");
            }

            var objDto = mapper.Map<AsientosContablesDto>(dataItem);

            await outputCacheStore.EvictByTagAsync(AC.EvictByTagAsientosContables, default);

            return TypedResults.Created($"/asientoscontables/{uid}", objDto);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NotFound<string>, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, AsientosContablesDtoUpdate modelDtoUpdate
            , IRepositorioAsientoContable repo, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IServicioUsuarios srvUser
            , IValidator<AsientosContablesDtoUpdate> validator)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            var resultadoValidacion = await validator.ValidateAsync(modelDtoUpdate);
            if (!resultadoValidacion.IsValid)
            {
                return TypedResults.ValidationProblem(resultadoValidacion.ToDictionary());
            }

            var existe = await repo.Exist(id);
            if (!existe)
            {
                return TypedResults.NotFound("Transacción bancaria no encontrado");
            }

            await repo.Update(modelDtoUpdate);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagAsientosContables, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> Delete(
        Guid id,
        IRepositorioAsientoContable repo,
        IRepositorioAsientoContableDetalle repoChildren,
        IOutputCacheStore outputCacheStore,
        IServicioUsuarios srvUser)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }


            var objDB = await repo.GetById(id);

            if (objDB is null)
            {
                return TypedResults.NotFound("Asiento contable no encontrado");
            }

            //Eliminar los hijos
            await repoChildren.DeleteByParent(id);
            await repo.Delete(id);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagAsientosContables, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}