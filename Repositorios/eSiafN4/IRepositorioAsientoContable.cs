using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioAsientoContable
{
    Task<List<AsientosContables>> GetAlls(YearMonthParams queryParams);
    Task<AsientosContables?> GetById(Guid id);
}