using eSiafApiN4.Entidades.XanesN4;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios.XanesN4;

public interface IRepositorioQuotationDetailLegacy
{
    Task<List<QuotationDetailList>> GetAlls(QuotaParams queryParams);

}