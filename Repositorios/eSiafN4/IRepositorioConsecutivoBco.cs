using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConsecutivoBco
{
    Task<List<ConsecutivosBco>> GetAlls(QueryParams queryParams);
    Task<ConsecutivosBco?> GetById(Guid id);
    Task Update(ConsecutivosBcoDtoUpdate objUpdate);
    Task<bool> Exist(Guid id);
}