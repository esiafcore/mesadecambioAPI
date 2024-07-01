using eSiafApiN4.DTOs.XanesN8;
using eSiafApiN4.Utilidades;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eSiafApiN4.Endpoints.XanesN8;

public static class UsuariosEndpoints
{
    public static RouteGroupBuilder MapUsuarios(this RouteGroupBuilder group)
    {
        group.MapPost("/registrar", Registrar);
        return group;
    }

    static async Task<Results<Ok<RespuestaAutenticacionDto>
        , BadRequest<IEnumerable<IdentityError>>
        , ValidationProblem>> Registrar(
        CredencialesUsuarioDto credencialesUsuarioDto,
        [FromServices] UserManager<IdentityUser> userManager, IConfiguration configuration
        ,IValidator<CredencialesUsuarioDto> validator)
    {
        var resultadoValidacion = await validator.ValidateAsync(credencialesUsuarioDto);

        if (!resultadoValidacion.IsValid)
        {
            return TypedResults.ValidationProblem(resultadoValidacion.ToDictionary());
        }

        var usuario = new IdentityUser
        {
            UserName = credencialesUsuarioDto.Email,
            Email = credencialesUsuarioDto.Email
        };

        var resultado = await userManager.CreateAsync(usuario,credencialesUsuarioDto.Password);

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

    private async static Task<RespuestaAutenticacionDto>
        ConstruirToken(CredencialesUsuarioDto credencialesUsuarioDto,
            IConfiguration configuration)
    {
        var claims = new List<Claim>
        {
            new Claim("email",credencialesUsuarioDto.Email),
            new Claim("lo que yo quiera","cualquier otro valor")
        };
        var llave = Llaves.ObtenerLlave(configuration);
        var creds = new SigningCredentials(llave.First(), SecurityAlgorithms.HmacSha256);
        var expiracion = DateTime.UtcNow.AddHours(4);
        var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
            expires: expiracion, signingCredentials: creds);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);

        return new RespuestaAutenticacionDto
        {
            Token = token,
            Expiracion = expiracion
        };
    }
}