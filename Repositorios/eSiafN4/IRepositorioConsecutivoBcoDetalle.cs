using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using System.Linq.Expressions;
using XanesN8.Api.DTOs.eSiafN4;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConsecutivoBcoDetalle
{
    Task<List<ConsecutivosBcoDetalle>> GetAlls(YearMonthParams queryParams);
    Task<ConsecutivosBcoDetalle?> GetById(Guid id);
    Task Update(ConsecutivosBcoDetalleDtoUpdate objUpdate);
    Task<bool> Exist(Guid id);
}