using Microsoft.AspNetCore.Identity;

namespace eSiafApiN4.Repositorios.XanesN8;

public interface IRepositorioUsuarios
{
    Task<IdentityUser?> BuscarUsuarioPorEmail(string normalizedEmail);
    Task<string> Crear(IdentityUser usuario);
}