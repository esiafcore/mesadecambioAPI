using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using System.Linq.Expressions;
using XanesN8.Api.DTOs.eSiafN4;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConsecutivoCntDetalle
{
    Task<List<ConsecutivosCntDetalle>> GetAlls(YearMonthParams queryParams);
    Task<ConsecutivosCntDetalle?> GetById(Guid id);
    Task Update(ConsecutivosCntDetalleDtoUpdate objUpdate);
    Task<bool> Exist(Guid id);
}