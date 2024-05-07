using eSiafApiN4.Entidades;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public interface IRepositorioAsientoContable
{
    Task<List<AsientosContables>> GetAlls(AsientoContableParams queryParams);
    Task<AsientosContables?> GetById(Guid id);
}