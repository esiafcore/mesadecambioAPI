using eSiafApiN4.Entidades;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public interface IRepositorioAsientosContables
{
    Task<List<AsientosContables>> ObtenerTodos(AsientosContablesParams queryParams);
    Task<AsientosContables?> ObtenerPorId(Guid id);
}