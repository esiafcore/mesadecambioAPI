using AutoMapper;
using eSiafApiN4.Entidades.XanesN8;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios.XanesN8;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Endpoints.XanesN8;

public static class QuotationEndpoints
{
    public static RouteGroupBuilder MapQuotation(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60))
                .Tag("quotations-get"));

        return group;
    }

    static async Task<Results<Ok<List<QuotationsList>>, BadRequest<string>>> GetAlls(int companyId, DateTime beginDate, DateTime endDate
        , IRepositorioQuotation repositorio
        , IMapper mapper
        , int pagina = 1, int recordsPorPagina = 10)
    {
        DatesParams queryParams = new()
        {
            CompanyId = companyId,
            BeginDate = beginDate,
            EndDate = endDate,
            Pagina = pagina,
            RecordsPorPagina = recordsPorPagina
        };

        //Validar Rango de fecha
        if (queryParams.EndDate < queryParams.BeginDate)
        {
            return TypedResults.BadRequest($"Fecha final: {queryParams.EndDate.ToShortDateString()} no puede ser menor que fecha inicial: {queryParams.BeginDate.ToShortDateString()}");
        }

        try
        {
            var dataList = await repositorio.GetAlls(queryParams);
            return TypedResults.Ok(dataList);
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