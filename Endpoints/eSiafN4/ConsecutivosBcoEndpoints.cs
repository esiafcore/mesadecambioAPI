using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.LoggerManager;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.Servicios;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class ConsecutivosBcoEndpoints
{
    public static RouteGroupBuilder MapConsecutivosBco(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagConsecutivosBco))
            .RequireAuthorization();


        return group;
    }

    static async Task<Results<Ok<List<ConsecutivosBcoDto>>
        , NotFound<string>, BadRequest<string>>> GetAlls(Guid uidcia
        , IRepositorioConsecutivosBco repositorio
        , IMapper mapper, ILoggerManager logger
        , IServicioUsuarios srvUser
        , int yearfiscal = 0 ,int mesfiscal = 0
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
                Uidcia = uidcia,
                Pagina = pagina,
                Yearfiscal = yearfiscal,
                Mesfiscal = mesfiscal,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repositorio.GetAlls(queryParams);
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
            var errorMessage = $"Ha ocurrido un error el el método {nameof(GetAlls)} del Endpoint Consecutivos. Error: {ex.Message}";
            logger.LogError(errorMessage);
            return TypedResults.BadRequest(errorMessage);
        }
    }

}