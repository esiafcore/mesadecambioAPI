using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.FiltersParameters;
using static XanesN8.Api.Utilidades.Enumeradores;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioTransaccionBcoSubtipo
{
    Task<List<TransaccionesBcoSubtipos>> GetAlls(QueryParams queryParams);
    Task<TransaccionesBcoSubtipos?> GetById(Guid id);
    Task Update(TransaccionesBcoSubtipoDtoUpdate objUpdate);
    Task<bool> Exist(Guid id);
}