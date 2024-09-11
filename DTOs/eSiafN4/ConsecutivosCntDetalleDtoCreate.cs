﻿namespace XanesN8.Api.DTOs.eSiafN4;

public class ConsecutivosCntDetalleDtoCreate
{
    public Guid UidRegist { get; set; }

    public Guid UidCia { get; set; }

    public string Categoria { get; set; }

    public string Codigo { get; set; }

    public string NombreCampo { get; set; }

    public long Contador { get; set; }

    public long ContadorTemporal { get; set; }

    public string FormatoContador { get; set; }

    public string FormatoContadorTemporal { get; set; }

    public short ContadorPaddingIzquierdo { get; set; }

    public short ContadorTemporalPaddingIzquierdo { get; set; }

    public bool? IndAplicar { get; set; }

}