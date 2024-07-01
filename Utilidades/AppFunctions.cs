namespace eSiafApiN4.Utilidades;

public static class AppFunctions
{
    public static int CantidadTotalPaginas(int recordsPorPagina, int cantidadRegistros)
    {
        var cantidadTotalPaginas = 1;

        if (recordsPorPagina == 0) return cantidadTotalPaginas;
        var ultimaPagina = cantidadRegistros % recordsPorPagina;
        cantidadTotalPaginas = (cantidadRegistros / recordsPorPagina);
        cantidadTotalPaginas += ultimaPagina != 0 ? 1 : 0;

        return cantidadTotalPaginas;
    }
}