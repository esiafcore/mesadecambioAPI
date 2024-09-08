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

public static class ConfigBcoEndpoints
{
    public static RouteGroupBuilder MapConfigBco(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagConfigBco))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById).RequireAuthorization();

        return group;
    }

    static async Task<Results<Ok<List<ConfigBcoDto>>
        , NotFound<string>, BadRequest<string>>> GetAlls(IRepositorioConfigBco repo
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

            PaginationParams queryParams = new()
            {
                Pagina = pagina,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repo.GetAlls(queryParams);
            if (dataList.Count > 0)
            {
                var objList = mapper.Map<List<ConfigBcoDto>>(dataList);
                return TypedResults.Ok(objList);
            }
            else
            {
                var errorMessage = $"Método {nameof(GetAlls)} del Endpoint Configuración Bco. No hay datos que mostrar";
                logger.LogInfo(errorMessage);
                return TypedResults.NotFound(errorMessage);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Ha ocurrido un error el método {nameof(GetAlls)} del Endpoint Configuración Bco. Error: {ex.Message}";
            logger.LogError(errorMessage);
            return TypedResults.BadRequest(errorMessage);
        }
    }

    static async Task<Results<Ok<ConfigBcoDto>, NotFound
        , BadRequest<string>>> GetById(Guid id
        , IRepositorioConfigBco repo
        , IMapper mapper, IServicioUsuarios srvUser)
    {
        //Obtener usuario
        var usuario = await srvUser.ObtenerUsuario();

        if (usuario is null)
        {
            return TypedResults.BadRequest(AC.UserNotFound);
        }

        var dataItem = await repo.GetByCia(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<ConfigBcoDto>(dataItem);

        return TypedResults.Ok(objItem);
    }
}