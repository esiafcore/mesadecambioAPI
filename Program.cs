using eSiafApiN4.Entidades;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//Inicio de área de los servicios

builder.Services.AddCors(opts =>
{

    opts.AddDefaultPolicy(config =>
    {
        config.WithOrigins(origenesPermitidos)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

    opts.AddPolicy("libre", config =>
    {
        config.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositorioGeneros, RepositorioGeneros>();

//Fin de área de los servicios
var app = builder.Build();
//Inicio de área de los middleware

//if (builder.Environment.IsDevelopment())
//{

//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseOutputCache();

var endpointGeneros = app.MapGroup("/generos");

endpointGeneros.MapGet("/", ObtenerGeneros)
        .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60))
        .Tag("generos-get"));
endpointGeneros.MapGet("/{id:int}", ObtenerGeneroPorId);
endpointGeneros.MapPost("/", CrearGenero);
endpointGeneros.MapPut("/{id:int}", ActualizarGenero);
endpointGeneros.MapDelete("/{id:int}", BorrarGenero);


//Fin de área de los middleware
app.Run();

static async Task<Ok<List<Genero>>> ObtenerGeneros(IRepositorioGeneros repositorio)
{
    var generos = await repositorio.ObtenerTodos();
    return TypedResults.Ok(generos);
}

static async Task<Results<Ok<Genero>, NotFound>> ObtenerGeneroPorId(int id
        , IRepositorioGeneros repositorio)
{
    var genero = await repositorio.ObtenerPorId(id);
    if (genero is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(genero);
}

static async Task<Created<Genero>> CrearGenero(Genero genero
        , IRepositorioGeneros repositorioGeneros
        , IOutputCacheStore outputCacheStore)
{
    var id = await repositorioGeneros.Crear(genero);
    await outputCacheStore.EvictByTagAsync("generos-get", default);
    return TypedResults.Created($"/generos/{id}", genero);
}

static async Task<Results<NoContent, NotFound>> ActualizarGenero(int id, Genero genero
        , IRepositorioGeneros repositorio
        , IOutputCacheStore outputCacheStore)
{
    var existe = await repositorio.Existe(id);
    if (!existe)
    {
        return TypedResults.NotFound();
    }

    await repositorio.Actualizar(genero);
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