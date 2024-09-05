using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Utilidades;

namespace XanesN8.Api.Repositorios.eSiafN4;

public class RepositorioConsecutivoBcoDetalle : IRepositorioConsecutivoBcoDetalle
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioConsecutivoBcoDetalle(IConfiguration configuration
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

    public async Task<List<ConsecutivosBcoDetalle>> GetAlls(YearMonthParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<ConsecutivosBcoDetalle>(sql: @"bco.usp_consecutivosbcodetalle_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"bco.usp_consecutivosbcodetalle_count"
            , param: new { queryParams.Uidcia, queryParams.Yearfiscal, queryParams.Mesfiscal }
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

    public async Task<ConsecutivosBcoDetalle?> GetById(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<ConsecutivosBcoDetalle>(sql: @"bco.usp_consecutivosbcodetalle_getid"
                , param: new { uidregist = id }
                , commandType: CommandType.StoredProcedure);
        return dataItem;
    }

    public async Task Update(ConsecutivosBcoDetalle objUpdate)
    {
        using var conexion = new SqlConnection(_connectionString);

        Guid uidRegist = Guid.NewGuid();
        objUpdate.ModFch = DateTime.UtcNow;
        objUpdate.ModUsr = AC.LocalUserName;
        objUpdate.ModHsn = AC.LocHostMe;
        objUpdate.ModIps = AC.LocalIpv4Default;

        var idResult = await conexion.ExecuteAsync("bco.usp_consecutivosbcodetalle_update",
            objUpdate, commandType: CommandType.StoredProcedure);
        throw new NotImplementedException();
    }

    public async Task<bool> Exist(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);
        var existe = await conexion.QuerySingleAsync<bool>("bco.usp_consecutivosbcodetalle_isexist"
            , param: new { id }
            , commandType: CommandType.StoredProcedure);
        return existe;
    }
}