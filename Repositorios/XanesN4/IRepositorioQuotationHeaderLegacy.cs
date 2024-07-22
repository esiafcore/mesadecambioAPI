using XanesN8.Api.Entidades.XanesN4;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.XanesN4;

public interface IRepositorioQuotationHeaderLegacy
{
    Task<List<QuotationHeaderList>> GetAlls(QuotaParams queryParams);

}