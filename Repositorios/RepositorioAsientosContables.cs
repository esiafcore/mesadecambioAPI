using eSiafApiN4.Entidades;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public class RepositorioAsientosContables(IConfiguration configuration) : IRepositorioAsientosContables
{
    private readonly string? _connectionString = configuration.GetConnectionString("eSIAFN4Connection");

    public async Task<List<AsientosContables>> ObtenerTodos(AsientosContablesParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<AsientosContables>(sql: @"cnt.AsientosContables_GetAll"
            , param: queryParams, commandType: CommandType.StoredProcedure);
        return objList.ToList();
    }

    public async Task<AsientosContables?> ObtenerPorId(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion.QueryFirstOrDefaultAsync<AsientosContables>(sql: @"cnt.AsientosContables_GetId", param: new { uidregist = id}
            , commandType: CommandType.StoredProcedure);
        return dataItem;
    }
}