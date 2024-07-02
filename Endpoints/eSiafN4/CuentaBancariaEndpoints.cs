using AutoMapper;
using eSiafApiN4.DTOs.eSiafN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios.eSiafN4;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eSiafApiN4.Endpoints.eSiafN4;

public static class CuentaBancariaEndpoints
{
    public static RouteGroupBuilder MapCuentaBancaria(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagCuentasBancarias));
        group.MapGet("/{id:Guid}", GetById);

        return group;
    }

    static async Task<Ok<List<CuentasBancariasDto>>> GetAlls(Guid uidcia
        , IRepositorioCuentaBancaria repositorio
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
        var objList = mapper.Map<List<CuentasBancariasDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<CuentasBancariasDto>, NotFound>> GetById(Guid id
        , IRepositorioCuentaBancaria repositorio
        , IMapper mapper)
    {
        var dataItem = await repositorio.GetById(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<CuentasBancariasDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

}