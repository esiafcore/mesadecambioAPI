namespace eSiafApiN4.Utilidades;

public static class AppFunctions
{
    public static int CantidadTotalPaginas(int recordsPorPagina, int cantidadRegistros)
    {
        var ultimaPagina = cantidadRegistros % recordsPorPagina;
        var cantidadTotalPaginas = (cantidadRegistros / recordsPorPagina);
        cantidadTotalPaginas += ultimaPagina != 0 ? 1 : 0;
        return cantidadTotalPaginas;
    }
}