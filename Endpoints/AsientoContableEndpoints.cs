using AutoMapper;
using eSiafApiN4.DTOs;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eSiafApiN4.Endpoints;

public static class AsientoContableEndpoints
{
    public static RouteGroupBuilder MapAsientoContable(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60))
                .Tag("asientoscontables-get"));
        group.MapGet("/{id:Guid}", AsientoContableGetById);

        return group;
    }

    static async Task<Ok<List<AsientosContablesDto>>> GetAlls(Guid uidcia, int yearfiscal, int mesfiscal
        , IRepositorioAsientoContable repositorio
        , IMapper mapper
        , int pagina = 1, int recordsPorPagina = 10)
    {
        AsientoContableParams queryParams = new()
        {
            Uidcia = uidcia,
            Yearfiscal = yearfiscal,
            Mesfiscal = mesfiscal,
            Pagina = pagina,
            RecordsPorPagina = recordsPorPagina
        };

        var dataList = await repositorio.ObtenerTodos(queryParams);
        var objList = mapper.Map<List<AsientosContablesDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<AsientosContablesDto>, NotFound>> AsientoContableGetById(Guid id
        , IRepositorioAsientoContable repositorio
        , IMapper mapper)
    {
        var dataItem = await repositorio.ObtenerPorId(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<AsientosContablesDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

}