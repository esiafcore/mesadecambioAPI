using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConsecutivoBcoDetalle
{
    Task<List<ConsecutivosBcoDetalle>> GetAlls(YearMonthParams queryParams);
    Task<ConsecutivosBcoDetalle?> GetById(Guid id);
    Task Update(ConsecutivosBcoDetalle objUpdate);
    Task<bool> Exist(Guid id);
}