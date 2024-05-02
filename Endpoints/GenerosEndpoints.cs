using System.Runtime.CompilerServices;
using eSiafApiN4.DTOs;
using eSiafApiN4.Entidades;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace eSiafApiN4.Endpoints;

public static class GenerosEndpoints
{
    public static RouteGroupBuilder MapGeneros(this RouteGroupBuilder group)
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

    static async Task<Ok<List<GeneroDto>>> ObtenerGeneros(IRepositorioGeneros repositorio)
    {
        var generos = await repositorio.ObtenerTodos();
        var objList = generos
            .Select(x=> new GeneroDto
            {
                Id = x.Id,
                Nombre = x.Nombre
            }).ToList();

        return TypedResults.Ok(objList);
    }

    static async Task<Results<Ok<GeneroDto>, NotFound>> ObtenerGeneroPorId(int id
        , IRepositorioGeneros repositorio)
    {
        var genero = await repositorio.ObtenerPorId(id);
        if (genero is null)
        {
            return TypedResults.NotFound();
        }
        var objItem = new GeneroDto
        {
            Id = id,
            Nombre = genero.Nombre
        };

        return TypedResults.Ok(objItem);
    }

    static async Task<Created<GeneroDto>> CrearGenero(GeneroDtoUpsert objDto
        , IRepositorioGeneros repositorioGeneros
        , IOutputCacheStore outputCacheStore)
    {
        var objNew = new Genero
        {
            Nombre = objDto.Nombre
        };
        
        var id = await repositorioGeneros.Crear(objNew);
        await outputCacheStore.EvictByTagAsync("generos-get", default);
        var objItem = new GeneroDto
        {
            Id = id,
            Nombre = objNew.Nombre
        };
        return TypedResults.Created($"/generos/{id}", objItem);
    }

    static async Task<Results<NoContent, NotFound>> ActualizarGenero(int id, GeneroDtoUpsert objDto
        , IRepositorioGeneros repositorio
        , IOutputCacheStore outputCacheStore)
    {
        var existe = await repositorio.Existe(id);
        if (!existe)
        {
            return TypedResults.NotFound();
        }

        var objUpdated = new Genero
        {
            Id = id,
            Nombre = objDto.Nombre
        };

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