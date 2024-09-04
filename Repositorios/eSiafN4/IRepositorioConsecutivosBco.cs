using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioConsecutivosBco
{
    Task<List<ConsecutivosBco>> GetAlls(YearMonthParams queryParams);
    Task<ConsecutivosBco?> GetById(Guid id);
    Task Update(ConsecutivosBco objUpdate);

    //Task<bool> Exist(Guid id, string code);
    //Task<bool> Exist(Guid id);

}