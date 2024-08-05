using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using XanesN8.Api;
using XanesN8.Api.Entidades.XanesN8;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Utilidades;

namespace XanesN8.Api.Repositorios.XanesN8;

public class RepositorioQuotation : IRepositorioQuotation
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioQuotation(IConfiguration configuration
        , IHttpContextAccessor httpContextAccessor)
    {
        _connectionString = configuration.GetConnectionString(AC.XanesN8Cnx)!;
        _httpContext = httpContextAccessor.HttpContext!;
        var userId = configuration.GetValue<string>(AC.SecretUserId);
        var userPwd = configuration.GetValue<string>(AC.SecretUserPwd);
        var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString)
        {
            UserID = userId,
            Password = userPwd
        };
        _connectionString = connectionStringBuilder.ConnectionString;
    }

    public async Task<List<QuotationsList>> GetAlls(DatesParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<QuotationsList>(sql: @"fac.usp_quotations_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"fac.usp_quotations_count"
            , param: new { queryParams.CompanyId, queryParams.BeginDate, queryParams.EndDate }
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

    public async Task<List<DepositsList>> GetDeposits(DatesParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<DepositsList>(sql: @"fac.usp_deposits_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"fac.usp_deposits_count"
            , param: new { queryParams.CompanyId, queryParams.BeginDate, queryParams.EndDate }
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

    public async Task<List<TransfersList>> GetTransfers(DatesParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<TransfersList>(sql: @"fac.usp_transfers_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"fac.usp_transfers_count"
            , param: new { queryParams.CompanyId, queryParams.BeginDate, queryParams.EndDate }
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

}