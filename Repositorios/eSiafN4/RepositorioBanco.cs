using System.Data;
using Dapper;
using eSiafApiN4.Entidades.eSiafN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Utilidades;
using Microsoft.Data.SqlClient;

namespace eSiafApiN4.Repositorios.eSiafN4;

public class RepositorioBanco : IRepositorioBanco
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioBanco(IConfiguration configuration
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

    public async Task<List<Bancos>> GetAlls(QueryParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<Bancos>(sql: @"bco.usp_bancos_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"bco.usp_bancos_count"
            , param: new { queryParams.Uidcia }
            , commandType: CommandType.StoredProcedure);

        _httpContext.Response.Headers.Append("cantidadTotalRegistros",
            cantidadRegistros.ToString());

        if (cantidadRegistros > 0)
        {
            var cantidadTotalPaginas = AppFunctions.CantidadTotalPaginas(queryParams.RecordsPorPagina
                , cantidadRegistros);
            _httpContext.Response.Headers.Append("cantidadTotalPaginas",
                cantidadTotalPaginas.ToString());
        }

        return objList.ToList();
    }

    public async Task<Bancos?> GetById(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<Bancos>(sql: @"bco.usp_bancos_getid"
                , param: new { uidregist = id }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }

    public async Task<Guid> Create(Bancos objNew)
    {
        using var conexion = new SqlConnection(_connectionString);
        Guid uidRegist = Guid.NewGuid();
        objNew.CreFch = DateTime.UtcNow;
        objNew.CreUsr = AC.LocalUserName;
        objNew.CreHsn = AC.LocHostMe;
        objNew.CreIps = AC.Ipv4Default;

        var idResult = await conexion.QuerySingleAsync<int>("bco.usp_bancos_create",
            new
            {uidregist = uidRegist,objNew.UidCia,objNew.Codigo
            ,objNew.Descripci,objNew.IndTarjetaCredito
            ,objNew.NumeroObjeto,objNew.NumeroEstado
            ,objNew.CodigoOperacionSwitch,objNew.CuentaContableInterfazSwitch
            ,objNew.CodigoOperacionMantenimiento,objNew.CuentaContableInterfazMantenimiento
            ,objNew.ComisionBancariaPor
            ,objNew.CreFch,objNew.CreUsr
            ,objNew.CreHsn,objNew.CreIps
            },
            commandType: CommandType.StoredProcedure);

        return await Task.FromResult<Guid>(uidRegist);

    }

    public async Task Update(Bancos objUpdate)
    {
        using var conexion = new SqlConnection(_connectionString);

        Guid uidRegist = Guid.NewGuid();
        objUpdate.ModFch = DateTime.UtcNow;
        objUpdate.ModUsr = AC.LocalUserName;
        objUpdate.ModHsn = AC.LocHostMe;
        objUpdate.ModIps = AC.Ipv4Default;

        var idResult = await conexion.ExecuteAsync("bco.usp_bancos_update",
            objUpdate, commandType: CommandType.StoredProcedure);

    }

    public async Task<bool> Exist(Guid id, string code)
    {
        using var conexion = new SqlConnection(_connectionString);
        var existe = await conexion.QuerySingleAsync<bool>("bco.usp_bancos_isexistbycode"
            , param: new { id = id, codigo = code }
            , commandType: CommandType.StoredProcedure);
        return existe;
    }

    public async Task<bool> Exist(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);
        var existe = await conexion.QuerySingleAsync<bool>("bco.usp_bancos_isexist"
            , param: new {id}
            , commandType: CommandType.StoredProcedure);
        return existe;
    }

    public async Task Delete(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);
        await conexion.ExecuteAsync("bco.usp_bancos_delete", new { uidregist = id });
    }

}