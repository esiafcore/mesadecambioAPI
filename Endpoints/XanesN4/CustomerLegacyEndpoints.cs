using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.DTOs.XanesN4;
using XanesN8.Api.Repositorios.XanesN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Endpoints.XanesN4;

public static class CustomerLegacyEndpoints
{
    public static RouteGroupBuilder MapCustomerLegacy(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagCustomersLegacy))
            .RequireAuthorization();

        return group;
    }

    static async Task<Results<Ok<List<CustomerDto>>, BadRequest<string>>> GetAlls(IRepositorioCustomerLegacy repo
        , IMapper mapper, int pagina = 1, int recordsPorPagina = 10)
    {
        QueryParams queryParams = new()
        {
            Uidcia = Guid.Empty,
            Pagina = pagina,
            RecordsPorPagina = recordsPorPagina
        };

        try
        {
            var dataList = await repo.GetAlls(queryParams);
            var objList = mapper.Map<List<CustomerDto>>(dataList);

            return TypedResults.Ok(objList);
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