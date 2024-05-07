using System.Runtime.CompilerServices;
using AutoMapper;
using eSiafApiN4.DTOs;
using eSiafApiN4.Entidades;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace eSiafApiN4.Endpoints;

public static class GeneroEndpoints
{
    public static RouteGroupBuilder MapGenero(this RouteGroupBuilder group)
    {
        group.MapGet("/", ObtenerGeneros)
            .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60))
                .Tag("generos-get"));
        group.MapGet("/{id:int}", ObtenerGeneroPorId);
        group.MapPost("/", CrearGenero);
        group.MapPut("/{id:int}", ActualizarGenero);
        group.MapDelete("/{id:int}", BorrarGenero);
        return group;
    }

    static async Task<Ok<List<GeneroDto>>> ObtenerGeneros(IRepositorioGeneros repositorio
        ,IMapper mapper)
    {
        var generos = await repositorio.ObtenerTodos();
        var objList = mapper.Map<List<GeneroDto>>(generos);

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<GeneroDto>, NotFound>> ObtenerGeneroPorId(int id
        , IRepositorioGeneros repositorio
        , IMapper mapper)
    {
        var dataItem = await repositorio.ObtenerPorId(id);
        if (dataItem is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = mapper.Map<GeneroDto>(dataItem);

        return TypedResults.Ok(objItem);
    }

    static async Task<Created<GeneroDto>> CrearGenero(GeneroDtoCreate objCreate
        , IRepositorioGeneros repositorioGeneros
        , IOutputCacheStore outputCacheStore
        , IMapper mapper)
    {
        var id = await repositorioGeneros.Crear(objCreate);
        await outputCacheStore.EvictByTagAsync("generos-get", default);

        var objItem = mapper.Map<GeneroDto>(objCreate);
        objItem.Id = id;
        return TypedResults.Created($"/generos/{id}", objItem);
    }

    static async Task<Results<NoContent, NotFound>> ActualizarGenero(int id, GeneroDtoCreate objCreate
        , IRepositorioGeneros repositorio
        , IOutputCacheStore outputCacheStore
        , IMapper mapper)
    {
        var existe = await repositorio.Existe(id);
        if (!existe)
        {
            return TypedResults.NotFound();
        }

        var objUpdated = mapper.Map<GeneroDtoUpdate>(objCreate);
        objUpdated.Id = id;
        await repositorio.Actualizar(objUpdated);
        await outputCacheStore.EvictByTagAsync("generos-get", default);
        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, NotFound>> BorrarGenero(int id, IRepositorioGeneros repositorio
        , IOutputCacheStore outputCacheStore)
    {
        var existe = await repositorio.Existe(id);
        if (!existe)
        {
            return TypedResults.NotFound();
        }
        await repositorio.Borrar(id);
        await outputCacheStore.EvictByTagAsync("generos-get", default);
        return TypedResults.NoContent();
    }
}