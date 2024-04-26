using eSiafApiN4.Entidades;

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

//Fin de área de los servicios
var app = builder.Build();
//Inicio de área de los middleware

app.MapGet("/", () => "Hello World!");
app.MapGet("/otra-cosa", () => "!Hola, otra cosa");

app.MapGet("/generos", () =>
{
    var generos = new List<Genero>
    {
        new Genero { Id = 1, Nombre="Drama"},
        new Genero { Id = 2, Nombre="Acción"},
        new Genero { Id = 3, Nombre="Comedia"}
    };

    return generos;
});

//Fin de área de los middleware
app.Run();
