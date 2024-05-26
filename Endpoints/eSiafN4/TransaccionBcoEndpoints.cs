using AutoMapper;
using eSiafApiN4.DTOs.eSiafN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios.eSiafN4;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eSiafApiN4.Endpoints.eSiafN4;

public static class TransaccionBcoEndpoints
{
    public static RouteGroupBuilder MapTransaccionBco(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60))
                .Tag("transaccionesbco-get"));
        group.MapGet("/{id:Guid}", GetById);

        return group;
    }

    static async Task<Ok<List<TransaccionesBcoDto>>> GetAlls(Guid uidcia, int yearfiscal, int mesfiscal
        , IRepositorioTransaccionBco repositorio
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

        var dataList = await repositorio.GetAlls(queryParams);
        var objList = mapper.Map<List<TransaccionesBcoDto>>(dataList);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<TransaccionesBcoDto>, NotFound>> GetById(Guid id
        , IRepositorioTransaccionBco repositorio
        , IMapper mapper)
    {
        var dataItem = await repositorio.GetById(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<TransaccionesBcoDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

}