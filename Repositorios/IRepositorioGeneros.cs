using eSiafApiN4.Entidades;

namespace eSiafApiN4.Repositorios;

public interface IRepositorioGeneros
{
    Task<int> Crear(Genero genero);
    Task<List<Genero>> ObtenerTodos();
    Task<Genero?> ObtenerPorId(int id);
    Task<bool> Existe(int id);
    Task Actualizar(object genero);
}