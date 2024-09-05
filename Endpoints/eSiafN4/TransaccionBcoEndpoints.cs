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

        group.MapPost("/", Create)
            .DisableAntiforgery()
            .AddEndpointFilter<FiltroValidaciones<TransaccionesBcoDto>>()
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapPut("/{id:Guid}", Update)
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapDelete("/{id:Guid}", Delete)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Ok<List<TransaccionesBcoDto>>> GetAlls(Guid uidcia, int yearfiscal, int mesfiscal
        , IRepositorioTransaccionBco repositorio
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
        var objList = mapper.Map<List<TransaccionesBcoDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<TransaccionesBcoDto>, NotFound>> GetById(Guid id
        , IRepositorioTransaccionBco repositorio
        , IMapper mapper)
    {
        var dataItem = await repositorio.GetById(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<TransaccionesBcoDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

    static async Task<Results<Created<TransaccionesBcoDto>, BadRequest, NotFound, BadRequest<string>
       , ValidationProblem>>
       Create(TransaccionesBcoDtoCreate modelDtoCreate
       , IRepositorioTransaccionBco repositorio, IOutputCacheStore outputCacheStore
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

            var objDto = mapper.Map<TransaccionesBcoDto>(dataItem);
            
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancarias, default);
           
            return TypedResults.Created($"/transaccionesbco/{uid}", objDto);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NotFound, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, TransaccionesBcoDtoUpdate modelDtoUpdate
            , IRepositorioTransaccionBco repositorio, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IValidator<TransaccionesBcoDtoUpdate> validator)
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
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancarias, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NoContent, NotFound>> Delete(
        Guid id, 
        IRepositorioTransaccionBco repositorio,
        IOutputCacheStore outputCacheStore)
    {
        var objDB = await repositorio.GetById(id);

        if (objDB is null)
        {
            return TypedResults.NotFound();
        }

        //Eliminar los hijos

        await repositorio.Delete(id);
        await outputCacheStore.EvictByTagAsync(AC.EvictByTagTransaccionBancarias, default);
        return TypedResults.NoContent();
    }

}