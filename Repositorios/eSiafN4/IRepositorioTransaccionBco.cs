using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioTransaccionBco
{
    Task<List<TransaccionesBco>> GetAlls(YearMonthParams queryParams);
    Task<TransaccionesBco?> GetById(Guid id);
    Task<bool> GetIsAproval(Guid id);
    Task<Guid> Create(TransaccionesBcoDtoCreate obj);
    Task Update(TransaccionesBcoDtoUpdate obj);
    Task Delete(Guid id);
    Task<bool> Exist(Guid id);
}