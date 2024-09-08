using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using FluentValidation;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.Servicios;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.LoggerManager;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class ConsecutivosBcoEndpoints
{
    public static RouteGroupBuilder MapConsecutivosBco(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagConsecutivosBco))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById).RequireAuthorization();

        group.MapPut("/{id:Guid}", Update)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Results<Ok<List<ConsecutivosBcoDto>>
        , NotFound<string>, BadRequest<string>>> GetAlls(Guid uidcia
        , IRepositorioConsecutivoBco repo
        , IMapper mapper, ILoggerManager logger
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

            QueryParams queryParams = new()
            {
                Uidcia = uidcia,
                Pagina = pagina,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repo.GetAlls(queryParams);
            if (dataList.Count > 0)
            {
                var objList = mapper.Map<List<ConsecutivosBcoDto>>(dataList);
                return TypedResults.Ok(objList);
            }
            else
            {
                var errorMessage = $"Método {nameof(GetAlls)} del Endpoint Consecutivos Bco. No hay datos que mostrar";
                logger.LogInfo(errorMessage);
                return TypedResults.NotFound(errorMessage);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Ha ocurrido un error el método {nameof(GetAlls)} del Endpoint Consecutivos. Error: {ex.Message}";
            logger.LogError(errorMessage);
            return TypedResults.BadRequest(errorMessage);
        }
    }

    static async Task<Results<Ok<ConsecutivosBcoDto>, NotFound
        , BadRequest<string>>> GetById(Guid id
        , IRepositorioConsecutivoBco repo
        , IMapper mapper, IServicioUsuarios srvUser)
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
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<ConsecutivosBcoDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

    static async Task<Results<NotFound, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, ConsecutivosBcoDtoUpdate modelDtoUpdate
            , IRepositorioConsecutivoBco repo, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IValidator<ConsecutivosBcoDtoUpdate> validator)
    {
        try
        {
            var resultadoValidacion = await validator.ValidateAsync(modelDtoUpdate);
            if (!resultadoValidacion.IsValid)
            {
                return TypedResults.ValidationProblem(resultadoValidacion.ToDictionary());
            }

            var existe = await repo.Exist(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repo.Update(modelDtoUpdate);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagConsecutivosBco, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

}