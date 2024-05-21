﻿namespace eSiafApiN4.Utilidades;

public static class AppFunctions
{
    public static int CantidadTotalPaginas(int recordsPorPagina, int cantidadRegistros)
    {
        int ultimaPagina;
        int cantidadTotalPaginas = 1;

        if (recordsPorPagina !=  0)
        {
            ultimaPagina = cantidadRegistros % recordsPorPagina;
            cantidadTotalPaginas = (cantidadRegistros / recordsPorPagina);
            cantidadTotalPaginas += ultimaPagina != 0 ? 1 : 0;
        }

        return cantidadTotalPaginas;
    }
}