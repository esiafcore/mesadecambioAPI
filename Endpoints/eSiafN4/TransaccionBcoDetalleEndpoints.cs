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

public static class TransaccionBcoDetalleEndpoints
{
    public static RouteGroupBuilder MapTransaccionBcoDetalle(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagTransaccionBancariasDetalle))
            .RequireAuthorization();

        group.MapGet("getallbyparent/", GetAllByParent)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagTransaccionBancariasDetalle))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById)
            .RequireAuthorization();

        group.MapPost("/", Create)
            .DisableAntiforgery()
            .AddEndpointFilter<FiltroValidaciones<TransaccionesBcoDetalleDtoCreate>>()
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapPut("/{id:Guid}", Update)
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapDelete("/{id:Guid}", Delete)
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapDelete("deletebyparent/{id:Guid}", DeleteByParent)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Ok<List<TransaccionesBcoDetalleDto>>> GetAlls(Guid uidcia, int yearfiscal, int mesfiscal
        , IRepositorioTransaccionBcoDetalle repositorio
        , IMapper mapper
        , int pagina = 1, int recordsPorPagina = 10)
    {
        YearMonthParams queryParams = new()
        {
            Uidcia = uidcia,
            Yearfiscal = yearfiscal,
            Mesfiscal = mesfiscal,
            Pagina = pagina,
            RecordsPorPagina = recordsPorPagina
        };

        var dataList = await repositorio.GetAlls(queryParams);
        var objList = mapper.Map<List<TransaccionesBcoDetalleDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Ok<List<TransaccionesBcoDetalleDto>>> GetAllByParent(Guid uidparent, Guid uidcia, int yearfiscal, int mesfiscal
        , IRepositorioTransaccionBcoDetalle repositorio
        , IMapper mapper
        , int pagina = 1, int recordsPorPagina = 10)
    {
        ParentYearMonthParams queryParams = new()
        {
            UidParent = uidparent,
            Uidcia = uidcia,
            Yearfiscal = yearfiscal,
            Mesfiscal = mesfiscal,
            Pagina = pagina,
            RecordsPorPagina = recordsPorPagina
        };

        var dataList = await repositorio.GetAllByParent(queryParams);
        var objList = mapper.Map<List<TransaccionesBcoDetalleDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<TransaccionesBcoDetalleDto>, NotFound>> GetById(Guid id
        , IRepositorioTransaccionBcoDetalle repositorio
        , IMapper mapper)
    {
        var dataItem = await repositorio.GetById(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<TransaccionesBcoDetalleDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

    static async Task<Results<Created<TransaccionesBcoDetalleDto>, BadRequest, NotFound, BadRequest<string>
       , ValidationProblem>>
       Create(TransaccionesBcoDetalleDtoCreate modelDtoCreate
       , IRepositorioTransaccionBcoDetalle repositorio, IOutputCacheStore outputCacheStore
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

            var uid = await repositorio.Create(modelDtoCreate);

            var dataItem = await repositorio.GetById(uid);

            if (dataItem is null)
            {
                return TypedResults.NotFound();
            }

            var objDto = mapper.Map<TransaccionesBcoDetalleDto>(dataItem);

            await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancarias, default);

            return TypedResults.Created($"/transaccionesbcodetalle/{uid}", objDto);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NotFound, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, TransaccionesBcoDetalleDtoUpdate modelDtoUpdate
            , IRepositorioTransaccionBcoDetalle repositorio, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IValidator<TransaccionesBcoDetalleDtoUpdate> validator)
    {
        try
        {
            var resultadoValidacion = await validator.ValidateAsync(modelDtoUpdate);
            if (!resultadoValidacion.IsValid)
            {
                return TypedResults.ValidationProblem(resultadoValidacion.ToDictionary());
            }

            var existe = await repositorio.Exist(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Update(modelDtoUpdate);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancariasDetalle, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NoContent, NotFound>> Delete(
        Guid id,
        IRepositorioTransaccionBcoDetalle repositorio,
        IOutputCacheStore outputCacheStore)
    {
        var objDB = await repositorio.GetById(id);

        if (objDB is null)
        {
            return TypedResults.NotFound();
        }

        //Eliminar los hijos

        await repositorio.Delete(id);
        await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancariasDetalle, default);
        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, NotFound>> DeleteByParent(
        Guid id,
        IRepositorioTransaccionBcoDetalle repositorio,
        IRepositorioTransaccionBco repositorioParent,
        IOutputCacheStore outputCacheStore)
    {
        var objDB = await repositorioParent.GetById(id);

        if (objDB is null)
        {
            return TypedResults.NotFound();
        }

        //Eliminar los hijos

        await repositorio.DeleteByParent(id);
        await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancariasDetalle, default);
        return TypedResults.NoContent();
    }

}