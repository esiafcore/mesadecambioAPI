using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Repositorios.XanesN8;
using XanesN8.Api.Entidades.XanesN8;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Endpoints.XanesN8;

public static class QuotationEndpoints
{
    public static RouteGroupBuilder MapQuotation(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagQuotationsHeader))
                .RequireAuthorization();
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