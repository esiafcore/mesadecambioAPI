using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioTransaccionBcoDetalle
{
    Task<List<TransaccionesBcoDetalle>> GetAlls(YearMonthParams queryParams);
    Task<TransaccionesBcoDetalle?> GetById(Guid id);
    Task<List<TransaccionesBcoDetalle>> GetAllByParent(ParentYearMonthParams queryParams);
    Task<Guid> Create(TransaccionesBcoDetalleDtoCreate obj);
    Task Update(TransaccionesBcoDetalleDtoUpdate obj);
    Task Delete(Guid id);
    Task DeleteByParent(Guid id);
    Task<bool> Exist(Guid id);
}