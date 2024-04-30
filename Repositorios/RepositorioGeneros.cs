using Dapper;
using eSiafApiN4.Entidades;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Repositorios;

public class RepositorioGeneros : IRepositorioGeneros
{
    private readonly string? _connectionString;

    public RepositorioGeneros(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<int> CrearGenero(Genero genero)
    {
        using var conexion = new SqlConnection(_connectionString);

        var id = await conexion.QuerySingleAsync<int>(@"
                        INSERT INTO Generos (Nombre)
                        VALUES (@Nombre);

                        SELECT SCOPE_IDENTITY();

                ", genero);
        genero.Id = id;
        return id;
    }
}
