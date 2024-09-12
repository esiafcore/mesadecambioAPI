using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.Servicios;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.LoggerManager;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class ModuloEndpoints
{
    public static RouteGroupBuilder MapModulo(this RouteGroupBuilder group)
    {

        group.MapGet("/{id:Guid}", GetById).RequireAuthorization();
        group.MapGet("getbycode/", GetByCode).RequireAuthorization();
        group.MapGet("getbynumber/", GetByNumber).RequireAuthorization();
        return group;
    }

    static async Task<Results<Ok<ModulosDto>, NotFound<string>
        , BadRequest<string>>> GetById(Guid id
        , IRepositorioModulo repo
        , IMapper mapper, IServicioUsuarios srvUser)
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
                return TypedResults.NotFound("Modulo no encontrado");
            }

            var objItem = mapper.Map<ModulosDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<ModulosDto>, NotFound<string>
        , BadRequest<string>>> GetByNumber(Guid companyId, int numero
        , IRepositorioModulo repo
        , IMapper mapper, IServicioUsuarios srvUser)
    {

        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            var dataItem = await repo.GetByNumber(companyId, numero);
            if (dataItem is null)
            {
                return TypedResults.NotFound("Modulo no encontrado");
            }

            var objItem = mapper.Map<ModulosDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<ModulosDto>, NotFound<string>
        , BadRequest<string>>> GetByCode(Guid companyId, string codigo
        , IRepositorioModulo repo
        , IMapper mapper, IServicioUsuarios srvUser)
    {

        try
        {
            //Obtener usuario
            var usuario = await srvUser.ObtenerUsuario();

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.UserNotFound);
            }

            var dataItem = await repo.GetByCode(companyId, codigo);
            if (dataItem is null)
            {
                return TypedResults.NotFound("Modulo no encontrado");
            }

            var objItem = mapper.Map<ModulosDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}