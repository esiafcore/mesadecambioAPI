using eSiafApiN4.Entidades;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public class RepositorioAsientoContable(IConfiguration configuration) : IRepositorioAsientoContable
{
    private readonly string? _connectionString = configuration.GetConnectionString("eSIAFN4Connection");

    public async Task<List<AsientosContables>> ObtenerTodos(AsientoContableParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<AsientosContables>(sql: @"cnt.AsientoContable_GetAll"
            , param: queryParams, commandType: CommandType.StoredProcedure);
        return objList.ToList();
    }

    public async Task<AsientosContables?> ObtenerPorId(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion.QueryFirstOrDefaultAsync<AsientosContables>(sql: @"cnt.AsientoContable_GetId", param: new { uidregist = id}
            , commandType: CommandType.StoredProcedure);
        return dataItem;
    }
}