using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class AsientoContableEndpoints
{
    public static RouteGroupBuilder MapAsientoContable(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagAsientosContables))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById)
            .RequireAuthorization(AC.IsAdminClaim, AC.IsPowerUserClaim
            , AC.IsOperatorClaim);

        return group;
    }

    static async Task<Results<Ok<List<AsientosContablesDto>>, BadRequest<string>>> GetAlls(Guid companyId, int yearfiscal, int mesfiscal
        , IRepositorioAsientoContable repo
        , IMapper mapper
        , int pagina = 1, int recordsPorPagina = 10)
    {
        YearMonthParams queryParams = new()
        {
            Uidcia = companyId,
            Yearfiscal = yearfiscal,
            Mesfiscal = mesfiscal,
            Pagina = pagina,
            RecordsPorPagina = recordsPorPagina
        };

        try
        {
            var dataList = await repo.GetAlls(queryParams);
            var objList = mapper.Map<List<AsientosContablesDto>>(dataList);
            return TypedResults.Ok(objList);
        }
        catch (SqlException e)
        {
            return TypedResults.BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<AsientosContablesDto>, NotFound, BadRequest<string>>> GetById(Guid id
        , IRepositorioAsientoContable repo
        , IMapper mapper)
    {
        try
        {
            var dataItem = await repo.GetById(id);
            if (dataItem is null)
            {
                return TypedResults.NotFound();
            }
            var objItem = mapper.Map<AsientosContablesDto>(dataItem);

            return TypedResults.Ok(objItem);
        }
        catch (SqlException e)
        {
            return TypedResults.BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

}