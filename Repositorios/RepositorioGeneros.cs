using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using eSiafApiN4.DTOs;
using eSiafApiN4.Entidades;


namespace eSiafApiN4.Repositorios;

public class RepositorioGeneros(IConfiguration configuration) : IRepositorioGeneros
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<int> Crear(GeneroDtoCreate objCreate)
    {
        using var conexion = new SqlConnection(_connectionString);

        var id = await conexion.QuerySingleAsync<int>(sql: @"dbo.Generos_Create", param: objCreate
            , commandType: CommandType.StoredProcedure); ;
        return id;
    }

    public async Task<List<Genero>> ObtenerTodos()
    {
        using var conexion = new SqlConnection(_connectionString);

        var generos = await conexion.QueryAsync<Genero>(sql:@"dbo.Generos_GetAll"
                ,commandType: CommandType.StoredProcedure);
        return generos.ToList();
    }

    public async Task<Genero?> ObtenerPorId(int id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var genero = await conexion.QueryFirstOrDefaultAsync<Genero>(sql: @"dbo.Generos_GetId", param:new {id}
        ,commandType: CommandType.StoredProcedure);
        return genero;
    }

    public async Task<bool> Existe(int id)
    {
        using var conexion = new SqlConnection(_connectionString);
        var existe = await conexion.QuerySingleAsync<bool>(sql: @"dbo.Generos_IsExist", param:new {id}
        ,commandType: CommandType.StoredProcedure);
        return existe;
    }

    public async Task Actualizar(GeneroDtoUpdate objUpdate)
    {
        using var conexion = new SqlConnection(_connectionString);

        await conexion.ExecuteAsync(sql:@"dbo.Generos_Update",param:objUpdate
            ,commandType: CommandType.StoredProcedure);
    }

    public async Task Borrar(int id)
    {
        using var conexion = new SqlConnection(_connectionString);

        await conexion.ExecuteAsync(sql:@"dbo.Generos_Delete", param:new { id }
        ,commandType: CommandType.StoredProcedure);
     }
}
