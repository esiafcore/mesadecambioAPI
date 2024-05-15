using eSiafApiN4.Entidades.eSiafN4;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios.eSiafN4;

public interface IRepositorioCuentaBancaria
{
    Task<List<CuentasBancarias>> GetAlls(QueryParams queryParams);
    Task<CuentasBancarias?> GetById(Guid id);
}