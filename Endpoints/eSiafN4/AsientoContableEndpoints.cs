using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Servicios;
using FluentValidation;
using Microsoft.AspNetCore.OutputCaching;
using XanesN8.Api.Filtros;

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

        //group.MapGet("getnextsecuentialnumber/", GetNextSecuentialNumber)
        //    .RequireAuthorization();

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