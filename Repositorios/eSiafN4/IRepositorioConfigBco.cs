using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConfigBco
{
    Task<List<ConfigBco>> GetAlls(QueryParams queryParams);
    Task<ConfigBco?> GetById(Guid id);
    Task Update(ConfigBco objUpdate);
    Task<bool> Exist(Guid id);
}