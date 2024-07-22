using Microsoft.IdentityModel.Tokens;
using XanesN8.Api;

namespace XanesN8.Api.Utilidades;

public static class Llaves
{

    public static IEnumerable<SecurityKey> ObtenerLlave(IConfiguration configuration)
        => ObtenerLlave(configuration, AC.IssuedApp);

    public static IEnumerable<SecurityKey> ObtenerLlave(IConfiguration configuration, string issuer)
    {
        var signingKey = configuration.GetSection(AC.SectionKeys)
            .GetChildren()
            .SingleOrDefault(llave => llave[AC.IssuedByKey] == issuer);

        if (signingKey?[AC.IssuedByValue] is string valorLlave)
        {
            yield return new SymmetricSecurityKey(Convert.FromBase64String(valorLlave));
        }
    }

    public static IEnumerable<SecurityKey> ObtenerTodasLasLlaves(IConfiguration configuration)
    {
        var signingKeys = configuration.GetSection(AC.SectionKeys)
            .GetChildren();

        foreach (var signingKey in signingKeys)
        {
            if (signingKey?[AC.IssuedByValue] is string valorLlave)
            {
                yield return new SymmetricSecurityKey(Convert.FromBase64String(valorLlave));
            }
        }
    }
}