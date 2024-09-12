using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioModulo
{
    Task<Modulos?> GetByCode(Guid companyId, string codigo);
    Task<Modulos?> GetById(Guid id);
    Task<Modulos?> GetByNumber(Guid companyId, int numero);
}