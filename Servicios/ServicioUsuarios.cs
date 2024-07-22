using Microsoft.AspNetCore.Identity;
using XanesN8.Api;

namespace XanesN8.Api.Servicios;

public class ServicioUsuarios : IServicioUsuarios
{
    private readonly IHttpContextAccessor _httpAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public ServicioUsuarios(IHttpContextAccessor httpAccessor,
        UserManager<IdentityUser> userManager)
    {
        _httpAccessor = httpAccessor;
        _userManager = userManager;
    }

    public async Task<IdentityUser?> ObtenerUsuario()
    {
        var emailClaim = _httpAccessor.HttpContext!
            .User.Claims.FirstOrDefault(x => x.Type == AC.TypeClaimEmail);

        if (emailClaim is null)
        {
            return null;
        }

        var email = emailClaim.Value;
        return await _userManager.FindByEmailAsync(email);
    }

}