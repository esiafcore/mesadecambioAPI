using eSiafApiN4.Entidades.eSiafN4;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public interface IRepositorioAsientoContable
{
    Task<List<AsientosContables>> GetAlls(YearMonthParams queryParams);
    Task<AsientosContables?> GetById(Guid id);
}