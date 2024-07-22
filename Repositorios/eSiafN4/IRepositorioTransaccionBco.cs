using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioTransaccionBco
{
    Task<List<TransaccionesBco>> GetAlls(YearMonthParams queryParams);
    Task<TransaccionesBco?> GetById(Guid id);
}