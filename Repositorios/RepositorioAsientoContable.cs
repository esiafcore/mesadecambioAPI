using eSiafApiN4.Entidades;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public class RepositorioAsientoContable : IRepositorioAsientoContable
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioAsientoContable(IConfiguration configuration 
        ,IHttpContextAccessor httpContextAccessor)
    {
        _connectionString = configuration.GetConnectionString("eSIAFN4Connection")!;
        _httpContext = httpContextAccessor.HttpContext!;

    }

    public async Task<List<AsientosContables>> ObtenerTodos(AsientoContableParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<AsientosContables>(sql: @"cnt.asientocontable_getall"
            , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"cnt.asientocontable_count"
            , param: new { queryParams.Uidcia, queryParams.Yearfiscal, queryParams.Mesfiscal }
            , commandType: CommandType.StoredProcedure);

        _httpContext.Response.Headers.Append("cantidadTotalRegistros",
            cantidadRegistros.ToString());

        if (cantidadRegistros > 0)
        {
            var ultimaPagina = cantidadRegistros % queryParams.RecordsPorPagina;
            var cantidadTotalPaginas = (cantidadRegistros / queryParams.RecordsPorPagina);
            cantidadTotalPaginas += ultimaPagina != 0 ? 1 : 0;
            _httpContext.Response.Headers.Append("cantidadTotalPaginas",
                cantidadTotalPaginas.ToString());
        }

        return objList.ToList();
    }

    public async Task<AsientosContables?> ObtenerPorId(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<AsientosContables>(sql: @"cnt.asientocontable_getid"
            , param: new { uidregist = id}
            , commandType: CommandType.StoredProcedure);
        return dataItem;
    }
}