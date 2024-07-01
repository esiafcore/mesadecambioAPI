using AutoMapper;
using eSiafApiN4.DTOs.eSiafN4;
using eSiafApiN4.Entidades.eSiafN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios.eSiafN4;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using FluentValidation;
using eSiafApiN4.LoggerManager;

namespace eSiafApiN4.Endpoints.eSiafN4;

public static class BancoEndpoints
{
    private static readonly string _evictByTag = "bancos-get";
    private static readonly int _cacheOutputExpire = 15;

    public static RouteGroupBuilder MapBanco(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(_cacheOutputExpire))
                .Tag(_evictByTag));//.RequireAuthorization();
        group.MapGet("/{id:Guid}", GetById);
        group.MapPost("/", Create)
            .DisableAntiforgery();
        group.MapPut("/{id:Guid}", Update);

        group.MapDelete("/{id:Guid}", Delete);

        return group;
    }

    static async Task<Results<Ok<List<BancosDto>>
        , NotFound<string> ,BadRequest<string>>> GetAlls(Guid uidcia
        , IRepositorioBanco repositorio
        , IMapper mapper ,ILoggerManager logger
        , int pagina = 1, int recordsPorPagina = 10)
    {
        try
        {
            QueryParams queryParams = new()
            {
                Uidcia = uidcia,
                Pagina = pagina,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repositorio.GetAlls(queryParams);
            if (dataList.Count > 0 )
            {
                var objList = mapper.Map<List<BancosDto>>(dataList);
                return TypedResults.Ok(objList);
            }
            else
            {
                var errorMessage = $"Método {nameof(GetAlls)} del Endpoint Bancos. No hay datos que mostrar";
                logger.LogInfo(errorMessage);
                return TypedResults.NotFound(errorMessage);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Ha ocurrido un error el el método {nameof(GetAlls)} del Endpoint Bancos. Error: {ex.Message}";
            logger.LogError(errorMessage);
            return TypedResults.BadRequest(errorMessage);
        }
    }

    static async Task<Results<Ok<BancosDto>, NotFound>> GetById(Guid id
        , IRepositorioBanco repositorio
        , IMapper mapper)
    {
        var dataItem = await repositorio.GetById(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<BancosDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

    static async Task<Results<Created<BancosDto>, BadRequest, BadRequest<string>, ValidationProblem>>
        Create(BancosDtoCreate bancoDtoCreate
        , IRepositorioBanco repositorio, IOutputCacheStore outputCacheStore
        , IMapper mapper
        , IValidator<BancosDtoCreate> validator)
    {
        try
        {
            var resultadoValidacion = await validator.ValidateAsync(bancoDtoCreate);
            if (!resultadoValidacion.IsValid)
            {
                return TypedResults.ValidationProblem(resultadoValidacion.ToDictionary());
            }

            var objNew = mapper.Map<Bancos>(bancoDtoCreate);
            var id = await repositorio.Create(objNew);
            objNew.UidRegist = id;
            await outputCacheStore.EvictByTagAsync(_evictByTag, default);
            var objDto = mapper.Map<BancosDto>(objNew);
            return TypedResults.Created($"/bancos/{id}", objDto);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
        //SqlException,DbException,InvalidOperationException => e.Message 
    }

    static async Task<Results<NotFound, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, BancosDtoUpdate bancoDtoUpdate
            , IRepositorioBanco repositorio, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IValidator<BancosDtoUpdate> validator)
    {
        try
        {
            var resultadoValidacion = await validator.ValidateAsync(bancoDtoUpdate);
            if (!resultadoValidacion.IsValid)
            {
                return TypedResults.ValidationProblem(resultadoValidacion.ToDictionary());
            }

            var existe = await repositorio.Exist(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var objUpdate = mapper.Map<Bancos>(bancoDtoUpdate);

            await repositorio.Update(objUpdate);
            await outputCacheStore.EvictByTagAsync(_evictByTag, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NoContent, NotFound>> Delete(Guid id, IRepositorioBanco repositorio,
        IOutputCacheStore outputCacheStore)
    {
        var objDB = await repositorio.GetById(id);

        if (objDB is null)
        {
            return TypedResults.NotFound();
        }

        await repositorio.Delete(id);
        await outputCacheStore.EvictByTagAsync(_evictByTag, default);
        return TypedResults.NoContent();
    }

}