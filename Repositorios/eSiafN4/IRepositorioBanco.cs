using eSiafApiN4.Entidades.eSiafN4;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios.eSiafN4;

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