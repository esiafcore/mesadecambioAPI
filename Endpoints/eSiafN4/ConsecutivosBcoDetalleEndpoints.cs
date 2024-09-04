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

public static class ConsecutivosBcoDetalleEndpoints
{
    public static RouteGroupBuilder MapConsecutivosBcoDetalle(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagConsecutivosBcoDetalle))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById).RequireAuthorization();

        group.MapPut("/{id:Guid}", Update)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Results<Ok<List<ConsecutivosBcoDetalleDto>>
        , NotFound<string>, BadRequest<string>>> GetAlls(Guid uidcia
        , IRepositorioConsecutivosBcoDetalle repositorio
        , IMapper mapper, ILoggerManager logger
        , IServicioUsuarios srvUser
        , int yearfiscal = 0, int mesfiscal = 0
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
                var objList = mapper.Map<List<ConsecutivosBcoDetalleDto>>(dataList);
                return TypedResults.Ok(objList);
            }
            else
            {
                var errorMessage = $"Método {nameof(GetAlls)} del Endpoint Consecutivos Bco Detalle. No hay datos que mostrar";
                logger.LogInfo(errorMessage);
                return TypedResults.NotFound(errorMessage);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Ha ocurrido un error el método {nameof(GetAlls)} del Endpoint Consecutivos Bco Detalle. Error: {ex.Message}";
            logger.LogError(errorMessage);
            return TypedResults.BadRequest(errorMessage);
        }
    }

    static async Task<Results<Ok<ConsecutivosBcoDetalleDto>, NotFound
        , BadRequest<string>>> GetById(Guid id
        , IRepositorioConsecutivosBcoDetalle repositorio
        , IMapper mapper, IServicioUsuarios srvUser)
    {
        //Obtener usuario
        var usuario = await srvUser.ObtenerUsuario();

        if (usuario is null)
        {
            return TypedResults.BadRequest(AC.UserNotFound);
        }

        var dataItem = await repositorio.GetById(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<ConsecutivosBcoDetalleDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

    static async Task<Results<NotFound, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, ConsecutivosBcoDetalleDtoUpdate modelDtoUpdate
            , IRepositorioConsecutivosBcoDetalle repositorio, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IValidator<ConsecutivosBcoDetalleDtoUpdate> validator)
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

            var objUpdate = mapper.Map<ConsecutivosBcoDetalle>(modelDtoUpdate);

            await repositorio.Update(objUpdate);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagConsecutivosBcoDetalle, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

}