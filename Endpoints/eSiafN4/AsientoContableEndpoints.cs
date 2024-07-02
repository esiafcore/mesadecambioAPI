using AutoMapper;
using eSiafApiN4.DTOs.eSiafN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios.eSiafN4;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Endpoints.eSiafN4;

public static class AsientoContableEndpoints
{
    public static RouteGroupBuilder MapAsientoContable(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagAsientosContables));
        group.MapGet("/{id:Guid}", GetById);

        return group;
    }

    static async Task<Results<Ok<List<AsientosContablesDto>>, BadRequest<string>>> GetAlls(Guid uidcia, int yearfiscal, int mesfiscal
        , IRepositorioAsientoContable repositorio
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

        try
        {
            var dataList = await repositorio.GetAlls(queryParams);
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
        , IRepositorioAsientoContable repositorio
        , IMapper mapper)
    {
        try
        {
            var dataItem = await repositorio.GetById(id);
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