using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using XanesN8.Api;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Utilidades;
using XanesN8.Api.DTOs.eSiafN4;

namespace XanesN8.Api.Repositorios.eSiafN4;

public class RepositorioAsientoContable : IRepositorioAsientoContable
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioAsientoContable(IConfiguration configuration
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

    public async Task<List<AsientosContables>> GetAlls(YearMonthParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<AsientosContables>(sql: @"cnt.usp_asientoscontables_getall"
            , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"cnt.usp_asientoscontables_count"
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

    public async Task<AsientosContables?> GetById(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);

        var dataItem = await conexion
            .QueryFirstOrDefaultAsync<AsientosContables>(sql: @"cnt.usp_asientoscontables_getbyid"
            , param: new { uidregist = id }
            , commandType: CommandType.StoredProcedure);
        return dataItem;
    }

    public async Task<Guid> Create(AsientosContablesDtoCreate obj)
    {
        using var conexion = new SqlConnection(_connectionString);
        Guid uidRegist = Guid.NewGuid();
        obj.UidRegist = uidRegist;
        await conexion.ExecuteAsync(
            "cnt.usp_asientoscontables_create",
            obj,
            commandType: CommandType.StoredProcedure);

        return await Task.FromResult(uidRegist);
    }

    public async Task Update(AsientosContablesDtoUpdate obj)
    {
        using var conexion = new SqlConnection(_connectionString);

        await conexion.ExecuteAsync(
            "cnt.usp_asientoscontables_update",
            obj,
            commandType: CommandType.StoredProcedure);
    }

    public async Task Delete(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);
        await conexion.ExecuteAsync("cnt.usp_asientoscontables_delete", new { uidregist = id });
    }

    public async Task<bool> Exist(Guid id)
    {
        using var conexion = new SqlConnection(_connectionString);
        var existe = await conexion.QuerySingleAsync<bool>("cnt.usp_asientoscontables_isexist"
            , param: new { id }
            , commandType: CommandType.StoredProcedure);
        return existe;
    }
}