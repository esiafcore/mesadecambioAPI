using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using static XanesN8.Api.Utilidades.Enumeradores;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioTransaccionBcoTipo
{
    Task<List<TransaccionesBcoTipos>> GetAlls(QueryParams queryParams);
    Task<TransaccionesBcoTipos?> GetById(Guid id);
    Task Update(TransaccionesBcoTipoDtoUpdate objUpdate);
    Task<bool> Exist(Guid id);
}