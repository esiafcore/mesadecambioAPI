using eSiafApiN4.Entidades.XanesN4;
using eSiafApiN4.Entidades.XanesN8;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios.XanesN4;

public interface IRepositorioQuotationHeader
{
    Task<List<QuotationHeaderList>> GetAlls(DatesParams queryParams);

}