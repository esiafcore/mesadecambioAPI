﻿using Dapper;
using eSiafApiN4.Entidades.XanesN4;
using eSiafApiN4.FiltersParameters;
using eSiafApiN4.Utilidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace eSiafApiN4.Repositorios.XanesN4;

public class RepositorioQuotationHeaderLegacy(IConfiguration configuration
    , IHttpContextAccessor httpContextAccessor) : IRepositorioQuotationHeaderLegacy
{
    private readonly string _connectionString = configuration.GetConnectionString("XanesN4Connection")!;
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<List<QuotationHeaderList>> GetAlls(QuotaParams queryParams)
    {
        using var conexion = new SqlConnection(_connectionString);

        var objList = await conexion
            .QueryAsync<QuotationHeaderList>(sql: @"dbo.usp_quotationsheader_getall"
                , param: queryParams, commandType: CommandType.StoredProcedure);

        var cantidadRegistros = await conexion.QuerySingleAsync<int>(
            sql: @"dbo.usp_quotationsheader_count"
            , param: new { queryParams.BeginDate, queryParams.EndDate, queryParams.IdentificationNumber }
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