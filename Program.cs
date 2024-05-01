using eSiafApiN4.Entidades;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Cors;
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

    opts.AddPolicy("libre",config =>
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

app.MapGet("/", [EnableCors(policyName:"libre")]() => "Hello World!");
app.MapGet("/generos", [EnableCors(policyName: "libre")] async (IRepositorioGeneros repositorio)
    => await repositorio.ObtenerTodos())
        .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("generos-get"));

app.MapGet("/generos/{id:int}", async (int id
    , IRepositorioGeneros repositorio) =>
{
    var genero = await repositorio.ObtenerPorId(id);
    if (genero is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(genero);
});

app.MapPost("/generos", async (Genero genero
    , IRepositorioGeneros repositorioGeneros
    , IOutputCacheStore outputCacheStore) =>
{
    var id = await repositorioGeneros.Crear(genero);
    await outputCacheStore.EvictByTagAsync("generos-get", default);
    return TypedResults.Created($"/generos/{id}",genero);
});

app.MapPut("/generos/{id:int}", async (int id, Genero genero
    , IRepositorioGeneros repositorio
    ,IOutputCacheStore outputCacheStore) =>
{
    var existe = await repositorio.Existe(id);
    if (!existe)
    {
        return Results.NotFound();
    }

    await repositorio.Actualizar(genero);
    await outputCacheStore.EvictByTagAsync("generos-get", default);
    return Results.NoContent();
});

//Fin de área de los middleware
app.Run();
