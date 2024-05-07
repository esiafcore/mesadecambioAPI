using System.Data;
using Dapper;
using eSiafApiN4.Entidades;
using eSiafApiN4.FiltersParameters;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Repositorios;

public class RepositorioBanco : IRepositorioBanco
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioBanco(IConfiguration configuration
        , IHttpContextAccessor httpContextAccessor)
    {
        _connectionString = configuration.GetConnectionString("eSIAFN4Connection")!;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    public async Task<List<Bancos>> GetAlls(QueryParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<Bancos>(sql: @"bco.bancos_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"bco.bancos_count"
            , param: new { queryParams.Uidcia}
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

    public async Task<Bancos?> GetById(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<Bancos>(sql: @"bco.bancos_getid"
                , param: new { uidregist = id }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }
}