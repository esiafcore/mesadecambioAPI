using AutoMapper;
using eSiafApiN4.DTOs;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eSiafApiN4.Endpoints;

public static class AsientosContablesEndpoints
{
    public static RouteGroupBuilder MapAsientosContables(this RouteGroupBuilder group)
    {
        group.MapGet("/", ObtenerAsientosContables)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60))
                .Tag("asientoscontables-get"));
        group.MapGet("/{id:Guid}", ObtenerAsientosContablesPorId);

        return group;
    }

    static async Task<Ok<List<AsientosContablesDto>>> ObtenerAsientosContables(Guid uidcia, int yearfiscal, int mesfiscal
        , IRepositorioAsientosContables repositorio
        , IMapper mapper)
    {
        AsientosContablesParams queryParams = new()
        {
            uidcia = uidcia,
            yearfiscal = yearfiscal,
            mesfiscal = mesfiscal
        };

        var dataList = await repositorio.ObtenerTodos(queryParams);
        var objList = mapper.Map<List<AsientosContablesDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<AsientosContablesDto>, NotFound>> ObtenerAsientosContablesPorId(Guid id
        , IRepositorioAsientosContables repositorio
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