using AutoMapper;
using eSiafApiN4.Entidades.XanesN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios.XanesN4;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Endpoints.XanesN4;

public static class QuotationHeaderLegacyEndpoints
{
    public static RouteGroupBuilder MapQuotationHeaderLegacy(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60))
                .Tag("quotationsheaderlegacy-get"));

        return group;
    }

    static async Task<Results<Ok<List<QuotationHeaderList>>, BadRequest<string>>> GetAlls(DateTime beginDate
        , DateTime endDate
        , string? identificationNumber
        , IRepositorioQuotationHeaderLegacy repositorio
        , IMapper mapper
        , int pagina = 1, int recordsPorPagina = 10)
    {
        QuotaParams queryParams = new()
        {
            CompanyId = 0,
            BeginDate = beginDate,
            EndDate = endDate,
            IdentificationNumber = identificationNumber,
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