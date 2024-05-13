using eSiafApiN4.Entidades;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Utilidades;

namespace eSiafApiN4.Repositorios;

public class RepositorioAsientoContable(IConfiguration configuration
        , IHttpContextAccessor httpContextAccessor)
    : IRepositorioAsientoContable
{
    private readonly string _connectionString = configuration.GetConnectionString("eSIAFN4Connection")!;
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<List<AsientosContables>> GetAlls(YearMonthParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<AsientosContables>(sql: @"cnt.usp_asientoscontables_getall"
            , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"cnt.usp_asientoscontables_count"
            , param: new { queryParams.Uidcia, queryParams.Yearfiscal, queryParams.Mesfiscal }
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

    public async Task<AsientosContables?> GetById(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<AsientosContables>(sql: @"cnt.usp_asientoscontables_getid"
            , param: new { uidregist = id}
            , commandType: CommandType.StoredProcedure);
        return dataItem;
    }
}