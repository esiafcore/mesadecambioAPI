using Microsoft.AspNetCore.Identity;

namespace XanesN8.Api.Servicios;

public interface IServicioUsuarios
{
    Task<IdentityUser?> ObtenerUsuario();
}