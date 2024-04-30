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

    public async Task<int> Crear(Genero genero)
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

    public async Task<List<Genero>> ObtenerTodos()
    {
        using var conexion = new SqlConnection(_connectionString);

        var generos = await conexion.QueryAsync<Genero>(@"
                        SELECT tbl.Id ,tbl.Nombre
                        FROM dbo.Generos as tbl;
                ");
        return generos.ToList();
    }

    public async Task<Genero?> ObtenerPorId(int id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var genero = await conexion.QueryFirstOrDefaultAsync<Genero>(@"
                        SELECT tbl.Id ,tbl.Nombre
                        FROM dbo.Generos as tbl
                        WHERE Id = @Id;", new {id});
        return genero;
    }

}
