using eSiafApiN4.Entidades;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Cors;

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
        .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15)));

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
    , IRepositorioGeneros repositorioGeneros) =>
{
    var id = await repositorioGeneros.Crear(genero);
    return TypedResults.Created($"/generos/{id}",genero);
});



//Fin de área de los middleware
app.Run();
