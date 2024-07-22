using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioBanco
{
    Task<List<Bancos>> GetAlls(QueryParams queryParams);
    Task<Bancos?> GetById(Guid id);
    Task<Guid> Create(Bancos objNew);
    Task Update(Bancos objUpdate);

    Task Delete(Guid id);
    Task<bool> Exist(Guid id, string code);
    Task<bool> Exist(Guid id);

}