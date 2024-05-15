using System.Data;
using Dapper;
using eSiafApiN4.Entidades.XanesN8;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Utilidades;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Repositorios.XanesN8;

public class RepositorioQuotation(IConfiguration configuration
    , IHttpContextAccessor httpContextAccessor) : IRepositorioQuotation
{
    private readonly string _connectionString = configuration.GetConnectionString("XanesN8Connection")!;
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<List<QuotationsList>> GetAlls(DatesParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<QuotationsList>(sql: @"fac.usp_quotations_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"fac.usp_quotations_count"
            , param: new { queryParams.CompanyId, queryParams.BeginDate, queryParams.EndDate }
            , commandType: CommandType.StoredProcedure);

        _httpContext.Response.Headers.Append("cantidadTotalRegistros",
            cantidadRegistros.ToString());

        if (cantidadRegistros > 0)
        {
            var cantidadTotalPaginas = AppFunctions.CantidadTotalPaginas(queryParams.RecordsPorPagina
                , cantidadRegistros);
            _httpContext.Response.Headers.Append("cantidadTotalPaginas",
                cantidadTotalPaginas.ToString());
        }
        return objList.ToList();
    }


}