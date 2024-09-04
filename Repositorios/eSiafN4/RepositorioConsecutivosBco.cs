using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Utilidades;

namespace XanesN8.Api.Repositorios.eSiafN4;

public class RepositorioConsecutivosBco : IRepositorioConsecutivosBco
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioConsecutivosBco(IConfiguration configuration
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

    public async Task<List<ConsecutivosBco>> GetAlls(YearMonthParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<ConsecutivosBco>(sql: @"bco.usp_consecutivosbco_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"bco.usp_consecutivosbco_count"
            , param: new { queryParams.Uidcia ,queryParams.Yearfiscal ,queryParams.Mesfiscal }
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

    public async Task<ConsecutivosBco?> GetById(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<ConsecutivosBco>(sql: @"bco.usp_consecutivosbco_getid"
                , param: new { uidregist = id }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }

    public async Task Update(ConsecutivosBco objUpdate)
    {
        using var conexion = new SqlConnection(_connectionString);

        Guid uidRegist = Guid.NewGuid();
        objUpdate.ModFch = DateTime.UtcNow;
        objUpdate.ModUsr = AC.LocalUserName;
        objUpdate.ModHsn = AC.LocHostMe;
        objUpdate.ModIps = AC.LocalIpv4Default;

        var idResult = await conexion.ExecuteAsync("bco.usp_consecutivosbco_update",
            objUpdate, commandType: CommandType.StoredProcedure);
        throw new NotImplementedException();
    }



}