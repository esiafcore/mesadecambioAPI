using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConsecutivoCnt
{
    Task<List<ConsecutivosCnt>> GetAlls(QueryParams queryParams);
    Task<ConsecutivosCnt?> GetById(Guid id);
    Task Update(ConsecutivosCntDtoUpdate objUpdate);
    Task<bool> Exist(Guid id);
}