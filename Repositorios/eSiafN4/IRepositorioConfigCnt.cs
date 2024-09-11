using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConfigCnt
{
    Task<List<ConfigCnt>> GetAlls(PaginationParams queryParams);
    Task<ConfigCnt?> GetByCia(Guid id);
}