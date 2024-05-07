using eSiafApiN4.Entidades;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios;

public interface IRepositorioBanco
{
    Task<List<Bancos>> GetAlls(QueryParams queryParams);
    Task<Bancos?> GetById(Guid id);
}