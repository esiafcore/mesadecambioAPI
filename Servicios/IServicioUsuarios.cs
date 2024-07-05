using Microsoft.AspNetCore.Identity;

namespace eSiafApiN4.Servicios;

public interface IServicioUsuarios
{
    Task<IdentityUser?> ObtenerUsuario();
}