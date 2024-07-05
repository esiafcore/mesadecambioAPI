using eSiafApiN4.CustomMiddleware;
using eSiafApiN4.Endpoints.eSiafN4;
using eSiafApiN4.Endpoints.XanesN4;
using eSiafApiN4.Endpoints.XanesN8;
using eSiafApiN4.LoggerManager;
using eSiafApiN4.Repositorios.eSiafN4;
using eSiafApiN4.Repositorios.XanesN4;
using eSiafApiN4.Repositorios.XanesN8;
using eSiafApiN4.Servicios;
using eSiafApiN4.Utilidades;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.Setup()
    .LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory()
        , "/nlog.config"));

var origenesPermitidos = builder.Configuration.GetValue<string>("AllowedHosts")!;
//Inicio de área de los servicios
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddCors(opts =>
{

    opts.AddDefaultPolicy(config =>
    {
        config.AllowAnyOrigin() //.WithOrigins(AllowedHosts)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILoggerManager,LoggerManager>();
builder.Services.AddScoped<IRepositorioAsientoContable, RepositorioAsientoContable>();
builder.Services.AddScoped<IRepositorioBanco, RepositorioBanco>();
builder.Services.AddScoped<IRepositorioTransaccionBco, RepositorioTransaccionBco>();
builder.Services.AddScoped<IRepositorioCuentaBancaria, RepositorioCuentaBancaria>();
builder.Services.AddScoped<IRepositorioQuotation, RepositorioQuotation>();
builder.Services.AddScoped<IRepositorioQuotationHeaderLegacy, RepositorioQuotationHeaderLegacy>();
builder.Services.AddScoped<IRepositorioQuotationDetailLegacy, RepositorioQuotationDetailLegacy>();
builder.Services.AddScoped<IRepositorioCustomerLegacy, RepositorioCustomerLegacy>();
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();

builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//Usar JWT Sin tener el sistema de usuario
builder.Services.AddAuthentication().AddJwtBearer(
    options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = Llaves.ObtenerLlave(builder.Configuration).First(),
            //IssuerSigningKeys = Llaves.ObtenerTodasLasLlaves(builder.Configuration),
            ClockSkew = TimeSpan.Zero
        };
    });

//Usar JWT con el sistema de usuario

//ValidateIssuer => Validar el emisor
//ValidateAudience => Validar audiencia
//ValidateLifetime => Validar el tiempo de vida del token
//ValidateIssuerSigningKey => Validar que el token esté debidamente firmado con la llave secreta
//ClockSkew => Se utiliza para no tener diferencia de tiempo a la hora de validar el token

builder.Services.AddAuthorization();

//Configurar Identity a nivel de los Servicios
builder.Services.AddTransient<IUserStore<IdentityUser>, UsuarioStore>();
//Configurar Política de Administración de Contraseña
builder.Services.AddIdentityCore<IdentityUser>(opciones =>
{
    opciones.Password.RequireDigit = true;
    opciones.Password.RequireLowercase = true;
    opciones.Password.RequireUppercase = true;
    opciones.Password.RequireNonAlphanumeric = true;
    opciones.Password.RequiredLength = 10;
}).AddDefaultTokenProviders();

builder.Services.AddTransient<SignInManager<IdentityUser>>();

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

app.UseAuthorization();

app.MapGroup("/asientoscontables").MapAsientoContable();
app.MapGroup("/bancos").MapBanco();
app.MapGroup("/transaccionesbco").MapTransaccionBco();
app.MapGroup("/cuentasbancarias").MapCuentaBancaria();
app.MapGroup("/quotations").MapQuotation();
app.MapGroup("/quotationslegacy").MapQuotationHeaderLegacy();
app.MapGroup("/quotationsdetaillegacy").MapQuotationDetailLegacy();
app.MapGroup("/customerslegacy").MapCustomerLegacy();
app.MapGroup("/usuarios").MapUsuarios();

//Fin de área de los middleware
app.Run();

