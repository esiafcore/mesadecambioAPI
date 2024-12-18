﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.Servicios;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.LoggerManager;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class ConfigCntEndpoints
{
    public static RouteGroupBuilder MapConfigCnt(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagConfigCnt))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById).RequireAuthorization();

        return group;
    }

    static async Task<Results<Ok<List<ConfigCntDto>>
        , NotFound<string>, BadRequest<string>>> GetAlls(IRepositorioConfigCnt repo
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
                var objList = mapper.Map<List<ConfigCntDto>>(dataList);
                return TypedResults.Ok(objList);
            }
            else
            {
                var errorMessage = $"Método {nameof(GetAlls)} del Endpoint Configuración Cnt. No hay datos que mostrar";
                logger.LogInfo(errorMessage);
                return TypedResults.NotFound(errorMessage);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Ha ocurrido un error el método {nameof(GetAlls)} del Endpoint Configuración Cnt. Error: {ex.Message}";
            logger.LogError(errorMessage);
            return TypedResults.BadRequest(errorMessage);
        }
    }

    static async Task<Results<Ok<ConfigCntDto>, NotFound<string>
        , BadRequest<string>>> GetById(Guid id
        , IRepositorioConfigCnt repo
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

            var dataItem = await repo.GetByCia(id);
            if (dataItem is null)
            {
                return TypedResults.NotFound("Configuración de contabilidad no encontrada");
            }
            var objItem = mapper.Map<ConfigCntDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}