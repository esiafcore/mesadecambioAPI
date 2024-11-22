using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using XanesN8.Api;
using XanesN8.Api.Filtros;
using XanesN8.Api.Utilidades;
using XanesN8.Api.DTOs.XanesN8;

namespace XanesN8.Api.Endpoints.XanesN8;

public static class UsuariosEndpoints
{
    public static RouteGroupBuilder MapUsuarios(this RouteGroupBuilder group)
    {
        group.MapPost("/registrar", Registrar)
            .AddEndpointFilter<FiltroValidaciones<CredencialesUsuarioDto>>();
        group.MapPost("/login", Login)
            .AddEndpointFilter<FiltroValidaciones<CredencialesUsuarioDto>>();

        return group;
    }

    static async Task<Results<Ok<RespuestaAutenticacionDto>
        , BadRequest<IEnumerable<IdentityError>>
        , ValidationProblem>> Registrar(
        CredencialesUsuarioDto credencialesUsuarioDto,
        [FromServices] UserManager<IdentityUser> userManager, IConfiguration configuration
        )
    {
        credencialesUsuarioDto.Email = credencialesUsuarioDto.Email.Trim();
        credencialesUsuarioDto.Password = credencialesUsuarioDto.Password.Trim();
        var usuario = new IdentityUser
        {
            UserName = credencialesUsuarioDto.Email,
            Email = credencialesUsuarioDto.Email
        };

        var resultado = await userManager.CreateAsync(usuario, credencialesUsuarioDto.Password);

        if (resultado.Succeeded)
        {
            var credencialesRespuesta = await ConstruirToken(credencialesUsuarioDto, configuration);
            return TypedResults.Ok(credencialesRespuesta);
        }
        else
        {
            return TypedResults.BadRequest(resultado.Errors);
        }
    }

    static async Task<Results<Ok<RespuestaAutenticacionDto>
        , BadRequest<string>>> Login(
        CredencialesUsuarioDto credencialesUsuarioDto
        , [FromServices] SignInManager<IdentityUser> signInManager,
        [FromServices] UserManager<IdentityUser> userManager
        , IConfiguration configuration
    )
    {

        credencialesUsuarioDto.Email = credencialesUsuarioDto.Email.Trim();
        credencialesUsuarioDto.Password = credencialesUsuarioDto.Password.Trim();

        try
        {
            var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDto.Email);

            if (usuario is null)
            {
                return TypedResults.BadRequest(AC.LoginIncorrectMessage);
            }

            //lockoutOnFailure: false. Por el momento si el usuario intenta varias veces que no lo bloquee
            var resultado = await signInManager.CheckPasswordSignInAsync(usuario,
                credencialesUsuarioDto.Password, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var respuestaAutenticacion =
                    await ConstruirToken(credencialesUsuarioDto, configuration);
                return TypedResults.Ok(respuestaAutenticacion);
            }
            else
            {
                return TypedResults.BadRequest(AC.LoginIncorrectMessage);
            }
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    private async static Task<RespuestaAutenticacionDto>
        ConstruirToken(CredencialesUsuarioDto credencialesUsuarioDto,
            IConfiguration configuration)
    {
        var claimsUser = new List<Claim>
        {
            new Claim(AC.TypeClaimEmail,credencialesUsuarioDto.Email)
        };

        var llave = Llaves.ObtenerLlave(configuration);
        var creds = new SigningCredentials(llave.First(), SecurityAlgorithms.HmacSha256);

        //Cuando va a expirar el toquen
        var expiracion = DateTime.UtcNow.AddHours(configuration.GetValue<int>("ExpirationTimeSettings:TokenTimeExpire"));

        var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claimsUser,
            expires: expiracion, signingCredentials: creds);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);

        return new RespuestaAutenticacionDto
        {
            Token = token,
            Expiracion = expiracion
        };
    }



}