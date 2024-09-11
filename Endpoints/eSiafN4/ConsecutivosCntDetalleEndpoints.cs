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

public static class ConsecutivosCntDetalleEndpoints
{
    public static RouteGroupBuilder MapConsecutivosCntDetalle(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagConsecutivosCntDetalle))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById).RequireAuthorization();

        group.MapPut("/{id:Guid}", Update)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Results<Ok<List<ConsecutivosCntDetalleDto>>
        , NotFound<string>, BadRequest<string>>> GetAlls(Guid companyId
        , IRepositorioConsecutivoCntDetalle repo
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
                Uidcia = companyId,
                Pagina = pagina,
                Yearfiscal = yearfiscal,
                Mesfiscal = mesfiscal,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repo.GetAlls(queryParams);
            if (dataList.Count > 0)
            {
                var objList = mapper.Map<List<ConsecutivosCntDetalleDto>>(dataList);
                return TypedResults.Ok(objList);
            }
            else
            {
                var errorMessage = $"Método {nameof(GetAlls)} del Endpoint Consecutivos Cnt Detalle. No hay datos que mostrar";
                logger.LogInfo(errorMessage);
                return TypedResults.NotFound(errorMessage);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Ha ocurrido un error el método {nameof(GetAlls)} del Endpoint Consecutivos Cnt Detalle. Error: {ex.Message}";
            logger.LogError(errorMessage);
            return TypedResults.BadRequest(errorMessage);
        }
    }

    static async Task<Results<Ok<ConsecutivosCntDetalleDto>, NotFound<string>
        , BadRequest<string>>> GetById(Guid id
        , IRepositorioConsecutivoCntDetalle repo
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

            var dataItem = await repo.GetById(id);
            if (dataItem is null)
            {
                return TypedResults.NotFound("Detalle de consecutivo de contabilidad no encontrado");
            }
            var objItem = mapper.Map<ConsecutivosCntDetalleDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NotFound<string>, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, ConsecutivosCntDetalleDtoUpdate modelDtoUpdate
            , IRepositorioConsecutivoCntDetalle repo, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IServicioUsuarios srvUser
            , IValidator<ConsecutivosCntDetalleDtoUpdate> validator)
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
                return TypedResults.NotFound("Detalle de consecutivo de contabilidad no encontrado");
            }

            await repo.Update(modelDtoUpdate);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagConsecutivosCntDetalle, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

}