using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioAsientoContableDetalle
{
    Task<List<AsientosContablesDetalle>> GetAlls(YearMonthParams queryParams);
    Task<AsientosContablesDetalle?> GetById(Guid id);
    Task<List<AsientosContablesDetalle>> GetAllByParent(ParentYearMonthParams queryParams);
    Task<Guid> Create(AsientosContablesDetalleDtoCreate obj);
    Task Update(AsientosContablesDetalleDtoUpdate obj);
    Task Delete(Guid id);
    Task DeleteByParent(Guid id);
    Task<bool> Exist(Guid id);
}