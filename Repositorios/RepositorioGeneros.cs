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
                        FROM dbo.Generos as tbl
                        ORDER BY tbl.Nombre;
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

    public async Task<bool> Existe(int id)
    {
        using var conexion = new SqlConnection(_connectionString);
        var existe = await conexion.QuerySingleAsync<bool>(@"
                        IF EXISTS(SELECT 1 FROM dbo.Generos AS tbl WHERE tbl.Id = @Id)
	                        SELECT 1 AS isExist
                        ELSE
	                        SELECT 0 AS isExist",new {id});
        return existe;
    }

    public async Task Actualizar(object genero)
    {
        using var conexion = new SqlConnection(_connectionString);

        await conexion.ExecuteAsync(@"
                                            UPDATE dbo.Generos
                                            SET Nombre = @Nombre
                                            WHERE Id = @Id
                                    ",genero);
    }

    public async Task Borrar(int id)
    {
        using var conexion = new SqlConnection(_connectionString);

        await conexion.ExecuteAsync(@"
                                        DELETE dbo.Generos
                                        WHERE Id = @Id
                                    ", new { id });
     }
}
