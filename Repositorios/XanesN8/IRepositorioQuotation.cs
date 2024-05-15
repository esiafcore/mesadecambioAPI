using eSiafApiN4.Entidades.XanesN8;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios.XanesN8;

public interface IRepositorioQuotation
{
    Task<List<QuotationsList>> GetAlls(DatesParams queryParams);
}