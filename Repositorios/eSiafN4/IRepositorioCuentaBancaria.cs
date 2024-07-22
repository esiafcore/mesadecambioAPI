using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioCuentaBancaria
{
    Task<List<CuentasBancarias>> GetAlls(QueryParams queryParams);
    Task<CuentasBancarias?> GetById(Guid id);
}