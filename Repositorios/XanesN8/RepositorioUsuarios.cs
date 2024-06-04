using System.Data;
using Dapper;
using eSiafApiN4.Entidades.XanesN8;
using eSiafApiN4.FiltersParameters;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Repositorios.XanesN8;

public class RepositorioUsuarios(IConfiguration configuration) : IRepositorioUsuarios
{
    private readonly string _connectionString = configuration.GetConnectionString("XanesN8Connection")!;

    public async Task<IdentityUser?> BuscarUsuarioPorEmail(string normalizedEmail)
    {
        await using var conexion = new SqlConnection(_connectionString);
        return await conexion.QuerySingleOrDefaultAsync<IdentityUser>(
            "cnf.usp_usuarios_buscarporemail", new {normalizedEmail},
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<string> Crear(IdentityUser usuario)
    {
        await using var conexion = new SqlConnection(_connectionString);
        usuario.Id = Guid.NewGuid().ToString();
        await conexion.ExecuteAsync("cnf.usp_usuarios_crear", new
        {
            usuario.Id,
            usuario.Email,
            usuario.NormalizedEmail,
            usuario.UserName,
            usuario.NormalizedUserName,
            usuario.PasswordHash
        }
        , commandType: CommandType.StoredProcedure);
        return usuario.Id;
    }

}