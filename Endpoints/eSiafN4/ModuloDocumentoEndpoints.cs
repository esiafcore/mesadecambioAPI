using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.Servicios;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.LoggerManager;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class ModuloDocumentoEndpoints
{
    public static RouteGroupBuilder MapModuloDocumento(this RouteGroupBuilder group)
    {

        group.MapGet("/{id:Guid}", GetById).RequireAuthorization();
        group.MapGet("getbycode/", GetByCode).RequireAuthorization();
        group.MapGet("getbynumber/", GetByNumber).RequireAuthorization();
        return group;
    }

    static async Task<Results<Ok<ModulosDocumentosDto>, NotFound<string>
        , BadRequest<string>>> GetById(Guid id
        , IRepositorioModuloDocumento repo
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
                return TypedResults.NotFound("Modulo documento no encontrado");
            }

            var objItem = mapper.Map<ModulosDocumentosDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<ModulosDocumentosDto>, NotFound<string>
        , BadRequest<string>>> GetByNumber(Guid companyId, Guid parentId, int numero
        , IRepositorioModuloDocumento repo
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

            var dataItem = await repo.GetByNumber(companyId, parentId, numero);
            if (dataItem is null)
            {
                return TypedResults.NotFound("Modulo documento no encontrado");
            }

            var objItem = mapper.Map<ModulosDocumentosDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<Ok<ModulosDocumentosDto>, NotFound<string>
        , BadRequest<string>>> GetByCode(Guid companyId, Guid parentId, string codigo
        , IRepositorioModuloDocumento repo
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

            var dataItem = await repo.GetByCode(companyId, parentId, codigo);
            if (dataItem is null)
            {
                return TypedResults.NotFound("Modulo documento no encontrado");
            }

            var objItem = mapper.Map<ModulosDocumentosDto>(dataItem);

            return TypedResults.Ok(objItem);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }
}