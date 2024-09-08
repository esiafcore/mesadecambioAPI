using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using FluentValidation;
using XanesN8.Api;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Repositorios.eSiafN4;
using XanesN8.Api.Servicios;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.Filtros;
using XanesN8.Api.FiltersParameters;
using XanesN8.Api.LoggerManager;

namespace XanesN8.Api.Endpoints.eSiafN4;

public static class BancoEndpoints
{
    //private static readonly string _evictByTag = "bancos-get";

    public static RouteGroupBuilder MapBanco(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAlls)
            .CacheOutput(c => c.Expire(TimeSpan.FromMinutes(AC.CacheOutputExpire))
                .Tag(AC.EvictByTagBancos))
                .RequireAuthorization();

        group.MapGet("/{id:Guid}", GetById).RequireAuthorization();

        group.MapPost("/", Create)
            .DisableAntiforgery()
            .AddEndpointFilter<FiltroValidaciones<BancosDto>>()
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapPut("/{id:Guid}", Update)
            .RequireAuthorization(AC.IsAdminClaim);

        group.MapDelete("/{id:Guid}", Delete)
            .RequireAuthorization(AC.IsAdminClaim);

        return group;
    }

    static async Task<Results<Ok<List<BancosDto>>
        , NotFound<string>, BadRequest<string>>> GetAlls(Guid uidcia
        , IRepositorioBanco repo
        , IMapper mapper, ILoggerManager logger
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
                Uidcia = uidcia,
                Pagina = pagina,
                RecordsPorPagina = recordsPorPagina
            };

            var dataList = await repo.GetAlls(queryParams);
            if (dataList.Count > 0)
            {
                var objList = mapper.Map<List<BancosDto>>(dataList);
                return TypedResults.Ok(objList);
            }
            else
            {
                var errorMessage = $"Método {nameof(GetAlls)} del Endpoint Bancos. No hay datos que mostrar";
                logger.LogInfo(errorMessage);
                return TypedResults.NotFound(errorMessage);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Ha ocurrido un error el método {nameof(GetAlls)} del Endpoint Bancos. Error: {ex.Message}";
            logger.LogError(errorMessage);
            return TypedResults.BadRequest(errorMessage);
        }
    }

    static async Task<Results<Ok<BancosDto>, NotFound
        , BadRequest<string>>> GetById(Guid id
        , IRepositorioBanco repo
        , IMapper mapper, IServicioUsuarios srvUser)
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
        var objItem = mapper.Map<BancosDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

    static async Task<Results<Created<BancosDto>, BadRequest, BadRequest<string>
        , ValidationProblem>>
        Create(BancosDtoCreate bancoDtoCreate
        , IRepositorioBanco repo, IOutputCacheStore outputCacheStore
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

            var objNew = mapper.Map<Bancos>(bancoDtoCreate);
            //Agregar usuario que crear el registro
            objNew.CreUsr = usuario.Email!;
            var id = await repo.Create(objNew);
            objNew.UidRegist = id;
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagBancos, default);
            var objDto = mapper.Map<BancosDto>(objNew);
            return TypedResults.Created($"/bancos/{id}", objDto);

        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
        //SqlException,DbException,InvalidOperationException => e.Message 
    }

    static async Task<Results<NotFound, BadRequest<string>, NoContent, ValidationProblem>>
        Update(Guid id, BancosDtoUpdate bancoDtoUpdate
            , IRepositorioBanco repo, IOutputCacheStore outputCacheStore
            , IMapper mapper
            , IValidator<BancosDtoUpdate> validator)
    {
        try
        {
            var resultadoValidacion = await validator.ValidateAsync(bancoDtoUpdate);
            if (!resultadoValidacion.IsValid)
            {
                return TypedResults.ValidationProblem(resultadoValidacion.ToDictionary());
            }

            var existe = await repo.Exist(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var objUpdate = mapper.Map<Bancos>(bancoDtoUpdate);

            await repo.Update(objUpdate);
            await outputCacheStore.EvictByTagAsync(AC.EvictByTagBancos, default);
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    static async Task<Results<NoContent, NotFound>> Delete(Guid id, IRepositorioBanco repo,
        IOutputCacheStore outputCacheStore)
    {
        var objDB = await repo.GetById(id);

        if (objDB is null)
        {
            return TypedResults.NotFound();
        }

        await repo.Delete(id);
        await outputCacheStore.EvictByTagAsync(AC.EvictByTagBancos, default);
        return TypedResults.NoContent();
    }

}