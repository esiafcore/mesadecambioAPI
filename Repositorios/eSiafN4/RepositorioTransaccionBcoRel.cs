using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using XanesN8.Api.DTOs.eSiafN4;

namespace XanesN8.Api.Repositorios.eSiafN4;

public class RepositorioTransaccionBcoRel : IRepositorioTransaccionBcoRel
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioTransaccionBcoRel(IConfiguration configuration
        , IHttpContextAccessor httpContextAccessor)
    {
        _connectionString = configuration.GetConnectionString(AC.EsiafN4Cnx)!;
        var userId = configuration.GetValue<string>(AC.SecretUserId);
        var userPwd = configuration.GetValue<string>(AC.SecretUserPwd);
        var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString)
        {
            UserID = userId,
            Password = userPwd
        };
        _connectionString = connectionStringBuilder.ConnectionString;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    public async Task<Guid> Create(TransaccionesBcoRelDtoCreate obj)
    {
        using var conexion = new SqlConnection(_connectionString);
        Guid uidRegist = Guid.NewGuid();
        obj.UidRegistro = uidRegist;
        await conexion.ExecuteAsync(
            "bco.usp_transaccionesbcorel_create",
            obj,
            commandType: CommandType.StoredProcedure);

        return await Task.FromResult(uidRegist);
    }

}