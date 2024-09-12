using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Utilidades;

namespace XanesN8.Api.Repositorios.eSiafN4;

public class RepositorioModulo : IRepositorioModulo
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioModulo(IConfiguration configuration
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

    public async Task<Modulos?> GetById(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<Modulos>(sql: @"cnf.usp_modulos_getbyid"
                , param: new { uidCia = id }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }

    public async Task<Modulos?> GetByCode(Guid companyId, string codigo)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<Modulos>(sql: @"cnf.usp_modulos_getbycode"
                , param: new { uidCia = companyId, codigo }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }

    public async Task<Modulos?> GetByNumber(Guid companyId, int numero)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<Modulos>(sql: @"cnf.usp_modulos_getbynumber"
                , param: new { uidCia = companyId, numero }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }
}