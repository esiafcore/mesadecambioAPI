using XanesN8.Api.DTOs.eSiafN4;

namespace XanesN8.Api.Repositorios.eSiafN4;

public interface IRepositorioTransaccionBcoRel
{
    Task<Guid> Create(TransaccionesBcoRelDtoCreate obj);
}