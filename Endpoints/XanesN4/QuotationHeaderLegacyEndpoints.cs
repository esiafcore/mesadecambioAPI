using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Repositorios.XanesN4;
using XanesN8.Api.Entidades.XanesN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Endpoints.XanesN4;

public static class QuotationHeaderLegacyEndpoints
{
    public static RouteGroupBuilder MapQuotationHeaderLegacy(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagQuotationsHeaderLegacy))
                .RequireAuthorization();

        return group;
    }

    static async Task<Results<Ok<List<QuotationHeaderList>>, BadRequest<string>>> GetAlls(DateTime beginDate
        , DateTime endDate
        , string? identificationNumber
        , IRepositorioQuotationHeaderLegacy repo
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
            var dataList = await repo.GetAlls(queryParams);
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