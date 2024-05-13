using eSiafApiN4.Endpoints;
using eSiafApiN4.Repositorios;
using FluentValidation;

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

builder.Services.AddScoped<IRepositorioGeneros, RepositorioGenero>();
builder.Services.AddScoped<IRepositorioAsientoContable, RepositorioAsientoContable>();
builder.Services.AddScoped<IRepositorioBanco, RepositorioBanco>();
builder.Services.AddScoped<IRepositorioTransaccionBco, RepositorioTransaccionBco>();
builder.Services.AddScoped<IRepositorioCuentaBancaria, RepositorioCuentaBancaria>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


//Fin de área de los servicios
var app = builder.Build();
//Inicio de área de los middleware

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseOutputCache();

//app.MapGroup("/generos").MapGenero();

app.MapGroup("/asientoscontables").MapAsientoContable();
app.MapGroup("/bancos").MapBanco();
app.MapGroup("/transaccionesbco").MapTransaccionBco();
app.MapGroup("/cuentasbancarias").MapCuentaBancaria();

//Fin de área de los middleware
app.Run();

