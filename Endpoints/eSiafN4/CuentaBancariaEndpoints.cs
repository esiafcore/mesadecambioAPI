using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class CuentaBancariaEndpoints
{
    public static RouteGroupBuilder MapCuentaBancaria(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagCuentasBancarias))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById)
            .RequireAuthorization();

        return group;
    }

    static async Task<Ok<List<CuentasBancariasDto>>> GetAlls(Guid uidcia
        , IRepositorioCuentaBancaria repo
        , IMapper mapper
        , int pagina = 1, int recordsPorPagina = 10)
    {
        QueryParams queryParams = new()
        {
            Uidcia = uidcia,
            Pagina = pagina,
            RecordsPorPagina = recordsPorPagina
        };

        var dataList = await repo.GetAlls(queryParams);
        var objList = mapper.Map<List<CuentasBancariasDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<CuentasBancariasDto>, NotFound>> GetById(Guid id
        , IRepositorioCuentaBancaria repo
        , IMapper mapper)
    {
        var dataItem = await repo.GetById(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<CuentasBancariasDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

}