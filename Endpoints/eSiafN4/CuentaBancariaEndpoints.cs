using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.Servicios;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class CuentaBancariaEndpoints
{
    public static RouteGroupBuilder MapCuentaBancaria(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagCuentasBancarias))
            .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById)
            .RequireAuthorization();

        group.MapGet("getallbybank/", GetAllByBank)
            .RequireAuthorization();

        return group;
    }

    static async Task<Results<Ok<List<CuentasBancariasDto>>, BadRequest<string>>> GetAlls(Guid companyId
        , IRepositorioCuentaBancaria repo
        , IMapper mapper
        , IServicioUsuarios srvUser
        , int pagina = 1, int recordsPorPagina = 10)
    {

        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            QueryParams queryParams = new()
            {
                Uidcia = companyId,
                Pagina = pagina,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repo.GetAlls(queryParams);
            var objList = mapper.Map<List<CuentasBancariasDto>>(dataList);

            return TypedResults.Ok(objList);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<CuentasBancariasDto>, NotFound, BadRequest<string>>> GetById(Guid id
        , IRepositorioCuentaBancaria repo
        , IMapper mapper
        , IServicioUsuarios srvUser)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            var dataItem = await repo.GetById(id);
            if (dataItem is null)
            {
                return TypedResults.NotFound();
            }
            var objItem = mapper.Map<CuentasBancariasDto>(dataItem);

            return TypedResults.Ok(objItem);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<List<CuentasBancariasDto>>,
        NotFound<string>, BadRequest<string>>> GetAllByBank(
        Guid companyId,
        string codigo,
        IRepositorioCuentaBancaria repo,
        IRepositorioBanco repoBanco,
        IMapper mapper,
        IServicioUsuarios srvUser)
    {
        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }
            
            if (codigo == string.Empty)
            {
                return TypedResults.BadRequest("El código es requerido");
            }

            QueryParams queryParams = new QueryParams
            {
                Uidcia = companyId,
                Pagina = 1,
                RecordsPorPagina = 0
            };

            var bankList = await repoBanco.GetAlls(queryParams);
            if (bankList is null || bankList.Count == 0)
            {
                return TypedResults.NotFound("Bancos no encontrados");
            }

            var bankModel = bankList.FirstOrDefault(x => x.Codigo.Trim() == codigo.Trim());
            if (bankModel is null)
            {
                return TypedResults.NotFound($"Banco:{codigo} no encontrado");
            }

            var bankAccountList = await repo.GetAlls(queryParams);
            if (bankAccountList is null || bankAccountList.Count == 0)
            {
                return TypedResults.NotFound("Cuentas bancarias no encontradas");
            }

            bankAccountList = bankAccountList
                .Where(x =>
                    x.UidBanco == bankModel.UidRegist).ToList();

            if (bankAccountList.Count == 0)
            {
                return TypedResults.NotFound("Cuentas bancarias no encontradas");
            }

            return TypedResults.Ok(mapper.Map<List<CuentasBancariasDto>>(bankAccountList));

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}