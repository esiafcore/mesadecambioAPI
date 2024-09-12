using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Utilidades;

namespace XanesN8.Api.Repositorios.eSiafN4;

public class RepositorioModuloDocumento : IRepositorioModuloDocumento
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioModuloDocumento(IConfiguration configuration
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

    public async Task<ModulosDocumentos?> GetById(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<ModulosDocumentos>(sql: @"cnf.usp_modulosdocumentos_getbyid"
                , param: new { uidCia = id }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }

    public async Task<ModulosDocumentos?> GetByCode(Guid companyId, Guid parentId, string codigo)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<ModulosDocumentos>(sql: @"cnf.usp_modulosdocumentos_getbycode"
                , param: new { uidCia = companyId, uidRegistPad = parentId, codigo }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }

    public async Task<ModulosDocumentos?> GetByNumber(Guid companyId, Guid parentId, int numero)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<ModulosDocumentos>(sql: @"cnf.usp_modulosdocumentos_getbynumber"
                , param: new { uidCia = companyId, uidRegistPad = parentId, numero }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }
}