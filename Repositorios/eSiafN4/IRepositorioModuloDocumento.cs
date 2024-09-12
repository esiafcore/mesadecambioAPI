using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioModuloDocumento
{
    Task<ModulosDocumentos?> GetByCode(Guid companyId, Guid parentId, string codigo);
    Task<ModulosDocumentos?> GetById(Guid id);
    Task<ModulosDocumentos?> GetByNumber(Guid companyId, Guid parentId, int numero);
}