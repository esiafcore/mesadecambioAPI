using eSiafApiN4.Entidades.eSiafN4;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public interface IRepositorioTransaccionBco
{
    Task<List<TransaccionesBco>> GetAlls(YearMonthParams queryParams);
    Task<TransaccionesBco?> GetById(Guid id);
}