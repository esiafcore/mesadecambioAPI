using eSiafApiN4.Entidades;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public interface IRepositorioCuentaBancaria
{
    Task<List<CuentasBancarias>> GetAlls(QueryParams queryParams);
    Task<CuentasBancarias?> GetById(Guid id);
}