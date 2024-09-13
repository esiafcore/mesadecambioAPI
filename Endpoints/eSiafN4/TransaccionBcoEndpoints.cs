using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Filtros;
using FluentValidation;
using Microsoft.AspNetCore.OutputCaching;
using XanesN8.Api.DTOs;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.Servicios;
using static XanesN8.Api.Utilidades.Enumeradores;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class TransaccionBcoEndpoints
{
    public static RouteGroupBuilder MapTransaccionBco(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagTransaccionBancarias))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById)
            .RequireAuthorization();

        group.MapGet("isaproval/", GetIsAproval)
            .RequireAuthorization();


        group.MapGet("getnextsecuentialnumber/", GetNextSecuentialNumber)
            .RequireAuthorization();

        group.MapPost("/", Create)
            .DisableAntiforgery()
            .AddEndpointFilter<FiltroValidaciones<TransaccionesBcoDtoCreate>>()
            .RequireAuthorization();

        group.MapPut("/{id:Guid}", Update)
            .DisableAntiforgery()
            .AddEndpointFilter<FiltroValidaciones<TransaccionesBcoDtoUpdate>>()
            .RequireAuthorization();

        group.MapDelete("/{id:Guid}", Delete)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Results<Ok<List<TransaccionesBcoDto>>, BadRequest<string>>> GetAlls(Guid companyId, int yearfiscal, int mesfiscal
        , IRepositorioTransaccionBco repo
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
            var objList = mapper.Map<List<TransaccionesBcoDto>>(dataList);

            return TypedResults.Ok(objList);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<TransaccionesBcoDto>, NotFound<string>, BadRequest<string>>> GetById(Guid id
        , IRepositorioTransaccionBco repo
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
                return TypedResults.NotFound("Transacción bancaria no encontrada");
            }

            var objItem = mapper.Map<TransaccionesBcoDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<bool>, NotFound<string>, BadRequest<string>>> GetIsAproval(Guid id
        , IRepositorioTransaccionBco repo
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

            var isAproval = await repo.GetIsAproval(id);

            return TypedResults.Ok(isAproval);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<TransaccionResponse>, NotFound<string>, BadRequest<string>>> GetNextSecuentialNumber(
        Guid companyId,
        Guid bankAccountId,
        int fiscalYear,
        int fiscalMonth,
        short tipo,
        short subtipo,
        IRepositorioTransaccionBco repo,
        IRepositorioConfigBco repoConfigBco,
        IRepositorioConsecutivoBco repoConsecutivoBco,
        IRepositorioConsecutivoBcoDetalle repoConsecutivoBcoDetalle,
        IRepositorioCuentaBancaria repoCuentaBancaria,
        IRepositorioTransaccionBcoTipo repoTipo,
        IRepositorioTransaccionBcoSubtipo repoSubtipo,
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

            TransaccionResponse response = new();
            int numberTransa = 0;
            string numberFull = string.Empty;
            CuentasBancarias? cuentaModel = new();
            ConsecutivosBco? consecutivoBcoModel = new();
            ConsecutivosBcoDetalle? consecutivoBcoDetalleModel = new();
            TransaccionesBcoTipos? transaccionBcoTipoModel = new();
            TransaccionesBcoSubtipos? transaccionBcoSubtipoModel = new();
            List<ConsecutivosBco>? consecutivoBcoList = new();
            List<ConsecutivosBcoDetalle>? consecutivoBcoDetalleList = new();
            List<TransaccionesBcoTipos>? transaccionBcoTipoList = new();
            List<TransaccionesBcoSubtipos>? transaccionBcoSubtipoList = new();

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

            var configBco = await repoConfigBco.GetByCia(companyId);
            if (configBco is null)
            {
                return TypedResults.NotFound("Configuración de banco no encontrada");
            }

            transaccionBcoTipoList = await repoTipo.GetAlls(queryParams);

            if (transaccionBcoTipoList is null || transaccionBcoTipoList.Count == 0)
            {
                return TypedResults.NotFound("Tipos de transacciones bancarias no encontrados");
            }

            transaccionBcoTipoModel = transaccionBcoTipoList
                .FirstOrDefault(x => x.Numero == tipo);

            if (transaccionBcoTipoModel is null)
            {
                return TypedResults.NotFound("Tipo de transaccion bancaria no encontrado");
            }

            transaccionBcoSubtipoList = await repoSubtipo.GetAlls(queryParams);

            if (transaccionBcoSubtipoList is null || transaccionBcoSubtipoList.Count == 0)
            {
                return TypedResults.NotFound("Subtipos de transacciones bancarias no encontrados");
            }

            transaccionBcoSubtipoModel = transaccionBcoSubtipoList
                .FirstOrDefault(x => 
                    x.UidRegistPad == transaccionBcoTipoModel.UidRegist &&
                    x.Numero == subtipo);

            if (transaccionBcoSubtipoModel is null)
            {
                return TypedResults.NotFound("Subtipo de transaccion bancaria no encontrado");
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
                    (BancoConsecutivoPor.Tipo | BancoConsecutivoPor.TipoMensual | BancoConsecutivoPor.TipoAnual);

                var consecutivoPor = (BancoConsecutivoPor)configBco.ConsecutivoTransaPor;

                //Si los consecutivos son por tipo
                if ((consecutivoPor & consecutivoTipo) == consecutivoPor)
                {
                    consecutivoBcoDetalleList = await repoConsecutivoBcoDetalle.GetAlls(yearMonthParams);

                    if (consecutivoBcoDetalleList is null || consecutivoBcoDetalleList.Count == 0)
                    {
                        return TypedResults.NotFound("Detalles de consecutivos no encontrados");
                    }

                    consecutivoBcoDetalleModel = consecutivoBcoDetalleList
                        .FirstOrDefault(x => x.Categoria.Trim() == AC.CategoryBcoByDefault &&
                                             x.Codigo.Trim() == transaccionBcoSubtipoModel.Codigo.Trim());

                    if (consecutivoBcoDetalleModel is null)
                    {
                        return TypedResults.NotFound($"Detalle de consecutivo: {AC.CategoryBcoByDefault}-{transaccionBcoSubtipoModel.Codigo.Trim()} no encontrado");
                    }

                    switch (consecutivo)
                    {
                        case ConsecutivoTipo.Temporal:
                            consecutivoBcoDetalleModel.ContadorTemporal++;
                            numberTransa = (int)consecutivoBcoDetalleModel.ContadorTemporal;
                            await repoConsecutivoBcoDetalle.Update(mapper.Map<ConsecutivosBcoDetalleDtoUpdate>(consecutivoBcoDetalleModel));

                            break;
                        case ConsecutivoTipo.Perpetuo:
                            consecutivoBcoDetalleModel.Contador++;
                            numberTransa = (int)consecutivoBcoDetalleModel.Contador;

                            if (isSave)
                                await repoConsecutivoBcoDetalle.Update(mapper.Map<ConsecutivosBcoDetalleDtoUpdate>(consecutivoBcoDetalleModel));

                            break;
                    }

                }
                else
                {
                    consecutivoBcoList = await repoConsecutivoBco.GetAlls(queryParams);

                    if (consecutivoBcoList is null || consecutivoBcoList.Count == 0)
                    {
                        return TypedResults.NotFound("Consecutivos no encontrados");
                    }

                    consecutivoBcoModel = consecutivoBcoList
                        .FirstOrDefault(x => x.Categoria.Trim() == AC.CategoryBcoByDefault &&
                                             x.Codigo.Trim() == AC.CategoryBcoByDefault);

                    if (consecutivoBcoModel is null)
                    {
                        return TypedResults.NotFound("Consecutivo no encontrado");
                    }

                    switch (consecutivo)
                    {
                        case ConsecutivoTipo.Temporal:
                            consecutivoBcoModel.ContadorTemporal++;
                            numberTransa = (int)consecutivoBcoModel.ContadorTemporal;
                            await repoConsecutivoBco.Update(mapper.Map<ConsecutivosBcoDtoUpdate>(consecutivoBcoModel));

                            break;
                        case ConsecutivoTipo.Perpetuo:
                            consecutivoBcoModel.Contador++;
                            numberTransa = (int)consecutivoBcoModel.Contador;

                            if (isSave)
                                await repoConsecutivoBco.Update(mapper.Map<ConsecutivosBcoDtoUpdate>(consecutivoBcoModel));

                            break;
                    }
                }
            }

            response.NumberTransa = numberTransa.ToString()
                .PadLeft(AC.TransactionTotalDigitsNumberDefault, AC.CharDefaultEmpty);
            response.TipoId = transaccionBcoTipoModel.UidRegist;
            response.SubtipoId = transaccionBcoSubtipoModel.UidRegist;

            return TypedResults.Ok(response);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Created<TransaccionesBcoDto>, NotFound<string>, BadRequest<string>
       , ValidationProblem>>
       Create(TransaccionesBcoDtoCreate modelDtoCreate
       , IRepositorioTransaccionBco repo, IOutputCacheStore outputCacheStore
       , IMapper mapper, IServicioUsuarios srvUser, IValidator<TransaccionesBcoDtoCreate> validator)
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
                return TypedResults.NotFound("Transacción bancaria no encontrada");
            }

            var objDto = mapper.Map<TransaccionesBcoDto>(dataItem);

            await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancarias, default);

            return TypedResults.Created($"/transaccionesbco/{uid}", objDto);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NotFound<string>, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, TransaccionesBcoDtoUpdate modelDtoUpdate
            , IRepositorioTransaccionBco repo, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IServicioUsuarios srvUser
            , IValidator<TransaccionesBcoDtoUpdate> validator)
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
                return TypedResults.NotFound("Transacción bancaria no encontrada");
            }

            await repo.Update(modelDtoUpdate);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancarias, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> Delete(
        Guid id,
        IRepositorioTransaccionBco repo,
        IRepositorioTransaccionBcoDetalle repoChildren,
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
                return TypedResults.NotFound("Transacción bancaria no encontrada");
            }

            //Eliminar los hijos
            await repoChildren.DeleteByParent(id);
            await repo.Delete(id);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancarias, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

}