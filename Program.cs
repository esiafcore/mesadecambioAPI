using eSiafApiN4.Entidades;
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

//Fin de área de los servicios
var app = builder.Build();
//Inicio de área de los middleware

app.UseCors();
app.UseOutputCache();

app.MapGet("/", [EnableCors(policyName:"libre")]() => "Hello World!");
app.MapGet("/generos", [EnableCors(policyName: "libre")] () =>
{
    var generos = new List<Genero>
    {
        new Genero { Id = 1, Nombre="Drama"},
        new Genero { Id = 2, Nombre="Acción"},
        new Genero { Id = 3, Nombre="Comedia"}
    };

    return generos;
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15)));

//Fin de área de los middleware
app.Run();
