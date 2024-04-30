using eSiafApiN4.Entidades;
using eSiafApiN4.Repositorios;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//Inicio de �rea de los servicios

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

//Fin de �rea de los servicios
var app = builder.Build();
//Inicio de �rea de los middleware

//if (builder.Environment.IsDevelopment())
//{

//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseOutputCache();

app.MapGet("/", [EnableCors(policyName:"libre")]() => "Hello World!");
app.MapGet("/generos", [EnableCors(policyName: "libre")] () =>
{
    var generos = new List<Genero>
    {
        new Genero { Id = 1, Nombre="Drama"},
        new Genero { Id = 2, Nombre="Acci�n"},
        new Genero { Id = 3, Nombre="Comedia"}
    };

    return generos;
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15)));

app.MapPost("/generos", async (Genero genero
    , IRepositorioGeneros repositorioGeneros) =>
{
    var id = await repositorioGeneros.CrearGenero(genero);
    return TypedResults.Created($"/generos/{id}",genero);
});

//Fin de �rea de los middleware
app.Run();
