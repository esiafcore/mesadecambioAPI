using Microsoft.AspNetCore.Identity;

namespace XanesN8.Api.Repositorios.XanesN8;

public interface IRepositorioUsuarios
{
    Task<IdentityUser?> BuscarUsuarioPorEmail(string normalizedEmail);
    Task<string> Crear(IdentityUser usuario);
}