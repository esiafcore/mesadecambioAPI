using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConfigBco
{
    Task<List<ConfigBco>> GetAlls(PaginationParams queryParams);
    Task<ConfigBco?> GetByCia(Guid id);
}