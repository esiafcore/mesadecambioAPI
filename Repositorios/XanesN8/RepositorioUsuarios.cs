using System.Data;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Repositorios.XanesN8;

public class RepositorioUsuarios : IRepositorioUsuarios
{
    private readonly string _connectionString;

    public RepositorioUsuarios(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(AC.XanesN8Cnx)!;
        var userId = configuration.GetValue<string>(AC.SecretUserId);
        var userPwd = configuration.GetValue<string>(AC.SecretUserPwd);
        var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString)
        {
            UserID = userId,
            Password = userPwd
        };
        _connectionString = connectionStringBuilder.ConnectionString;
    }

    public async Task<IdentityUser?> BuscarUsuarioPorEmail(string normalizedEmail)
    {
        await using var conexion = new SqlConnection(_connectionString);
        return await conexion.QuerySingleOrDefaultAsync<IdentityUser>(
            "cnf.usp_usuarios_buscarporemail", new {normalizedEmail},
            commandType: CommandType.StoredProcedure);
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