using eSiafApiN4.Entidades;

namespace eSiafApiN4.Repositorios;

public interface IRepositorioGeneros
{
    Task<int> CrearGenero(Genero genero);
}