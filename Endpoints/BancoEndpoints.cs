using AutoMapper;
using eSiafApiN4.DTOs;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eSiafApiN4.Endpoints;

public static class BancoEndpoints
{
    public static RouteGroupBuilder MapBanco(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60))
                .Tag("bancos-get"));
        group.MapGet("/{id:Guid}", GetById);

        return group;
    }

    static async Task<Ok<List<BancosDto>>> GetAlls(Guid uidcia
        , IRepositorioBanco repositorio
        , IMapper mapper
        , int pagina = 1, int recordsPorPagina = 10)
    {
        QueryParams queryParams = new()
        {
            Uidcia = uidcia,
            Pagina = pagina,
            RecordsPorPagina = recordsPorPagina
        };

        var dataList = await repositorio.GetAlls(queryParams);
        var objList = mapper.Map<List<BancosDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<BancosDto>, NotFound>> GetById(Guid id
        , IRepositorioBanco repositorio
        , IMapper mapper)
    {
        var dataItem = await repositorio.GetById(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<BancosDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

}