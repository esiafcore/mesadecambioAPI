using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Filtros;
using FluentValidation;
using Microsoft.AspNetCore.OutputCaching;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.Servicios;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class AsientoContableDetalleEndpoints
{
    public static RouteGroupBuilder MapAsientoContableDetalle(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagAsientosContablesDetalle))
            .RequireAuthorization();

        group.MapGet("getallbyparent/", GetAllByParent)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagAsientosContablesDetalle))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById)
            .RequireAuthorization();

        group.MapPost("/", Create)
            .DisableAntiforgery()
            .AddEndpointFilter<FiltroValidaciones<AsientosContablesDetalleDtoCreate>>()
            .RequireAuthorization();

        group.MapPut("/{id:Guid}", Update)
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapDelete("/{id:Guid}", Delete)
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapDelete("deletebyparent/{id:Guid}", DeleteByParent)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Results<Ok<List<AsientosContablesDetalleDto>>, BadRequest<string>>> GetAlls(Guid companyId, int yearfiscal, int mesfiscal
        , IRepositorioAsientoContableDetalle repo
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
            var objList = mapper.Map<List<AsientosContablesDetalleDto>>(dataList);

            return TypedResults.Ok(objList);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<List<AsientosContablesDetalleDto>>, BadRequest<string>>> GetAllByParent(Guid uidparent, Guid companyId, int yearfiscal, int mesfiscal
        , IRepositorioAsientoContableDetalle repo
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

            ParentYearMonthParams queryParams = new()
            {
                UidParent = uidparent,
                Uidcia = companyId,
                Yearfiscal = yearfiscal,
                Mesfiscal = mesfiscal,
                Pagina = pagina,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repo.GetAllByParent(queryParams);
            var objList = mapper.Map<List<AsientosContablesDetalleDto>>(dataList);

            return TypedResults.Ok(objList);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }

    }

    static async Task<Results<Ok<AsientosContablesDetalleDto>, NotFound<string>, BadRequest<string>>> GetById(Guid id
        , IRepositorioAsientoContableDetalle repo
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
                return TypedResults.NotFound("Detalle asiento contable no encontrado");
            }
            var objItem = mapper.Map<AsientosContablesDetalleDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Created<AsientosContablesDetalleDto>, NotFound<string>, BadRequest<string>
       , ValidationProblem>>
       Create(AsientosContablesDetalleDtoCreate modelDtoCreate
       , IRepositorioAsientoContableDetalle repo, IOutputCacheStore outputCacheStore
       , IMapper mapper, IServicioUsuarios srvUser)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            var uid = await repo.Create(modelDtoCreate);

            var dataItem = await repo.GetById(uid);

            if (dataItem is null)
            {
                return TypedResults.NotFound("Detalle asiento contable no encontrado");
            }

            var objDto = mapper.Map<AsientosContablesDetalleDto>(dataItem);

            await outputCacheStore.EvictByTagAsync(AC.EvictByTagAsientosContablesDetalle, default);

            return TypedResults.Created($"/asientoscontablesdetalle/{uid}", objDto);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NotFound<string>, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, AsientosContablesDetalleDtoUpdate modelDtoUpdate
            , IRepositorioAsientoContableDetalle repo, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IServicioUsuarios srvUser
            , IValidator<AsientosContablesDetalleDtoUpdate> validator)
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
                return TypedResults.NotFound("Detalle asiento contable no encontrado");
            }

            await repo.Update(modelDtoUpdate);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagAsientosContablesDetalle, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> Delete(
        Guid id,
        IRepositorioAsientoContableDetalle repo,
        IOutputCacheStore outputCacheStore, IServicioUsuarios srvUser)
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
                return TypedResults.NotFound("Detalle asiento contable no encontrado");
            }

            //Eliminar los hijos

            await repo.Delete(id);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagAsientosContablesDetalle, default);
            return TypedResults.NoContent();

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NoContent, NotFound<string>, BadRequest<string>>> DeleteByParent(
        Guid id,
        IRepositorioAsientoContableDetalle repo,
        IRepositorioAsientoContable repoParent,
        IOutputCacheStore outputCacheStore, IServicioUsuarios srvUser)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            var objDB = await repoParent.GetById(id);

            if (objDB is null)
            {
                return TypedResults.NotFound("Detalle asiento contable no encontrado");
            }

            //Eliminar los hijos

            await repo.DeleteByParent(id);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagAsientosContablesDetalle, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

}