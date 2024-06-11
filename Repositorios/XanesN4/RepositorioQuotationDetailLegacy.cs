using eSiafApiN4.Entidades.XanesN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Utilidades;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace eSiafApiN4.Repositorios.XanesN4;

public class RepositorioQuotationDetailLegacy : IRepositorioQuotationDetailLegacy
{
    private readonly string _connectionString;
    private readonly HttpContext _httpContext;

    public RepositorioQuotationDetailLegacy(IConfiguration configuration
        , IHttpContextAccessor httpContextAccessor)
    {
        _connectionString = configuration.GetConnectionString("XanesN4Connection")!;
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

    public async Task<List<QuotationDetailList>> GetAlls(QuotaParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<QuotationDetailList>(sql: @"dbo.usp_quotationsdetail_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"dbo.usp_quotationsdetail_count"
            , param: new { queryParams.BeginDate, queryParams.EndDate ,queryParams.IdentificationNumber }
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