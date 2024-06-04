using Microsoft.IdentityModel.Tokens;

namespace eSiafApiN4.Utilidades;

public static class Llaves
{

    public const string IssuerPropio = "esiafapin4-app";
    private const string SeccionLlaves = "Authentication:Schemes:Bearer:SigningKeys";
    private const string SeccionLlavesEmisor = "Issuer";
    private const string SeccionLlavesValor = "Value";

    public static IEnumerable<SecurityKey> ObtenerLlave(IConfiguration configuration)
        => ObtenerLlave(configuration, IssuerPropio);

    public static IEnumerable<SecurityKey> ObtenerLlave(IConfiguration configuration, string issuer)
    {
        var signingkey = configuration.GetSection(SeccionLlaves)
            .GetChildren()
            .SingleOrDefault(llave => llave[SeccionLlavesEmisor] == issuer);

        if (signingkey?[SeccionLlavesValor] is string valorLlave)
        {
            yield return new SymmetricSecurityKey(Convert.FromBase64String(valorLlave));
        }
    }

    public static IEnumerable<SecurityKey> ObtenerTodasLasLlaves(IConfiguration configuration)
    {
        var signingkeys = configuration.GetSection(SeccionLlaves)
            .GetChildren();

        foreach (var signingkey in signingkeys)
        {
            if (signingkey?[SeccionLlavesValor] is string valorLlave)
            {
                yield return new SymmetricSecurityKey(Convert.FromBase64String(valorLlave));
            }

        }
    }
}