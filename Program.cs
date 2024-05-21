using eSiafApiN4.CustomMiddleware;
using eSiafApiN4.Endpoints.eSiafN4;
using eSiafApiN4.Endpoints.XanesN8;
using eSiafApiN4.Repositorios.eSiafN4;
using eSiafApiN4.Repositorios.XanesN8;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//Inicio de área de los servicios
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddCors(opts =>
{

    opts.AddDefaultPolicy(config =>
    {
        config.AllowAnyOrigin() //.WithOrigins(origenesPermitidos)
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
builder.Services.AddScoped<IRepositorioQuotation, RepositorioQuotation>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//Fin de área de los servicios
var app = builder.Build();
app.UseMiddleware<ContentSecurityPolicyMiddleware>();
app.UseAntiforgery();

app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseOutputCache();

//app.MapGroup("/generos").MapGenero();

app.MapGroup("/asientoscontables").MapAsientoContable();
app.MapGroup("/bancos").MapBanco();
app.MapGroup("/transaccionesbco").MapTransaccionBco();
app.MapGroup("/cuentasbancarias").MapCuentaBancaria();
app.MapGroup("/quotations").MapQuotation();

//Fin de área de los middleware
app.Run();

