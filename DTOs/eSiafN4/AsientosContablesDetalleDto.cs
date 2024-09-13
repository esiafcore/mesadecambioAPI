﻿using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.DTOs.eSiafN4;

public class AsientosContablesDetalleDto
{
    public Guid UidRegist { get; set; }

    public Guid UidCia { get; set; }

    public Guid UidRegistPad { get; set; }

    public Guid UidCuentaContable { get; set; }

    public Guid? UidCentroCostoContable { get; set; }

    public Guid? UidAuxiliarContable { get; set; }

    public Guid? UidCuentaPresupuesto { get; set; }

    public Guid? UidCentroCostoPresupuesto { get; set; }

    public Guid? UidActividadProyecto { get; set; }

    public Guid? UidAuxiliarPresupuesto { get; set; }

    public Guid? UidDocumento { get; set; }

    public string CodigoDocumento { get; set; }

    public int NumeroLinea { get; set; }

    public short TipoMovimiento { get; set; }

    public decimal TipoCambioMonfor { get; set; }

    public decimal TipoCambioMonxtr { get; set; }

    public decimal TipoCambioParaMonfor { get; set; }

    public decimal TipoCambioParaMonxtr { get; set; }

    public decimal MontoMonbas { get; set; }

    public decimal MontoMonfor { get; set; }

    public decimal MontoMonxtr { get; set; }

    public Guid? UidBeneficiario { get; set; }

    public Guid? UidEntidad { get; set; }

    public short? TipoBeneficiario { get; set; }

    public Guid? UidCuentaBanco { get; set; }

    public Guid? UidArticulo { get; set; }

    public Guid? UidActivoFjio { get; set; }

    public Guid? UidSucursal { get; set; }

    public Guid? UidProyecto { get; set; }

    public bool IndDiferencial { get; set; }

    public string Comentarios { get; set; }

    public bool InddeCuadratura { get; set; }

    public DateTime CreFch { get; set; }

    public string CreUsr { get; set; }

    public string CreHsn { get; set; }

    public string CreHid { get; set; }

    public string CreIps { get; set; }

    public DateTime ModFch { get; set; }

    public string ModUsr { get; set; }

    public string ModHsn { get; set; }

    public string ModHid { get; set; }

    public string ModIps { get; set; }
}