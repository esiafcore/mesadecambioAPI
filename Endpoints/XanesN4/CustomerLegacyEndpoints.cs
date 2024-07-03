using AutoMapper;
using eSiafApiN4.DTOs.XanesN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Repositorios.XanesN4;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Endpoints.XanesN4;

public static class CustomerLegacyEndpoints
{
    public static RouteGroupBuilder MapCustomerLegacy(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag("customerslegacy-get"))
            .RequireAuthorization();

        return group;
    }

    static async Task<Results<Ok<List<CustomerDto>>, BadRequest<string>>> GetAlls(IRepositorioCustomerLegacy repositorio
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
            var dataList = await repositorio.GetAlls(queryParams);
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