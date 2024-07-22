using XanesN8.Api.Entidades.XanesN8;
using XanesN8.Api.FiltersParameters;

namespace XanesN8.Api.Repositorios.XanesN8;

public interface IRepositorioQuotation
{
    Task<List<QuotationsList>> GetAlls(DatesParams queryParams);
}