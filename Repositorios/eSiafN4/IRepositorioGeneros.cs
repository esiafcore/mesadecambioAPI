using eSiafApiN4.DTOs;
using eSiafApiN4.Entidades;

namespace eSiafApiN4.Repositorios.eSiafN4;

public interface IRepositorioGeneros
{
    Task<int> Crear(GeneroDtoCreate objCreate);
    Task<List<Genero>> ObtenerTodos();
    Task<Genero?> ObtenerPorId(int id);
    Task<bool> Existe(int id);
    Task Actualizar(GeneroDtoUpdate objUpdate);
    Task Borrar(int id);
}