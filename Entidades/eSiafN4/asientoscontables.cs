﻿using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4;

/// <summary>
/// Asientos contables
/// </summary>
public partial class AsientosContables
{

    /// <summary>
    /// Uid registro
    /// </summary>
    [Key]
    [Required()]
    public Guid UidRegist { get; set; }

    /// <summary>
    /// Uid cía
    /// </summary>
    [Required()]
    public Guid UidCia { get; set; }

    /// <summary>
    /// Uid módulo
    /// </summary>
    [Required()]
    public Guid UidModulo { get; set; }

    /// <summary>
    /// Uid documento
    /// </summary>
    [Required()]
    public Guid UidModuloDocumento { get; set; }

    /// <summary>
    /// # de moneda
    /// </summary>
    [Required()]
    public short NumeroMoneda { get; set; }

    /// <summary>
    /// # de estado
    /// </summary>
    [Required()]
    public int NumeroEstado { get; set; }

    /// <summary>
    /// # de objeto
    /// </summary>
    [Required()]
    public int NumeroObjeto { get; set; }

    /// <summary>
    /// Año fiscal
    /// </summary>
    [Required()]
    public short YearFiscal { get; set; }

    /// <summary>
    /// Mes fiscal
    /// </summary>
    [Required()]
    public short MesFiscal { get; set; }

    /// <summary>
    /// Fecha transacción
    /// </summary>
    [Required()]
    public DateTime FechaTransa { get; set; }

    /// <summary>
    /// # de transacción
    /// </summary>
    [StringLength(30)]
    [Required()]
    public string NumeroTransaccion { get; set; }

    /// <summary>
    /// Serie interna [B = Borrador. A= Aprobado/Impreso]
    /// </summary>
    [StringLength(1)]
    [Required()]
    public string SerieInterna { get; set; }

    /// <summary>
    /// # de transacción de referencia
    /// </summary>
    [StringLength(100)]
    public string NumeroTransaccionRef { get; set; }

    /// <summary>
    /// Tipo de cambio moneda foránea
    /// </summary>
    [Required()]
    public decimal TipoCambioMonfor { get; set; }

    /// <summary>
    /// Tipo de cambio moneda foránea adicional
    /// </summary>
    [Required()]
    public decimal TipoCambioMonxtr { get; set; }

    /// <summary>
    /// Tipo de cambio paralelo moneda foránea
    /// </summary>
    [Required()]
    public decimal TipoCambioparaMonfor { get; set; }

    /// <summary>
    /// Tipo de cambio paralelo moneda foránea adicional
    /// </summary>
    [Required()]
    public decimal TipoCambioparaMonxtr { get; set; }

    /// <summary>
    /// # de líneas
    /// </summary>
    public short? NumeroLineas { get; set; }

    /// <summary>
    /// Monto total débito moneda base
    /// </summary>
    [Required()]
    public decimal MontoDebitoMonbas { get; set; }

    /// <summary>
    /// Monto total débito moneda foránea
    /// </summary>
    [Required()]
    public decimal MontoDebitoMonfor { get; set; }

    /// <summary>
    /// Monto total débito moneda foránea adicional
    /// </summary>
    [Required()]
    public decimal MontoDebitoMonxtr { get; set; }

    /// <summary>
    /// Monto total crédito moneda base
    /// </summary>
    [Required()]
    public decimal MontoCreditoMonbas { get; set; }

    /// <summary>
    /// Monto total crédito moneda foránea
    /// </summary>
    [Required()]
    public decimal MontoCreditoMonfor { get; set; }

    /// <summary>
    /// Monto total crédito moneda foránea adicional
    /// </summary>
    [Required()]
    public decimal MontoCreditoMonxtr { get; set; }

    /// <summary>
    /// Uid proyecto
    /// </summary>
    public Guid? UidProyecto { get; set; }

    /// <summary>
    /// Uid Sucursal
    /// </summary>
    public Guid? UidSucursal { get; set; }

    /// <summary>
    /// Comentarios
    /// </summary>
    [Required()]
    public string Comentarios { get; set; }

    /// <summary>
    /// Fecha de anulación
    /// </summary>
    public DateTime? FechaTransaAnula { get; set; }

    /// <summary>
    /// # de transacción de anulación
    /// </summary>
    [StringLength(30)]
    public string NumeroTransaccionAnula { get; set; }

    /// <summary>
    /// Serie interna anula
    /// </summary>
    [StringLength(1)]
    public string SerieInternaAnula { get; set; }

    /// <summary>
    /// Año fiscal anulación
    /// </summary>
    public short? YearFiscalAnula { get; set; }

    /// <summary>
    /// Mes fiscal anulación
    /// </summary>
    public short? MesFiscalAnula { get; set; }

    /// <summary>
    /// Uid registro de referencia (Anulado/Reversado)
    /// </summary>
    public Guid? UidRegistref { get; set; }

    /// <summary>
    /// Indicador registro está correcto
    /// </summary>
    [Required()]
    public bool IndOkay { get; set; }

    /// <summary>
    /// Comentarios generados por el sistema
    /// </summary>
    public string ComentariosSistema { get; set; }

    /// <summary>
    /// Fecha de creación del registro
    /// </summary>
    [Required()]
    public DateTime CreFch { get; set; }

    /// <summary>
    /// Usuario creador del registro
    /// </summary>
    [StringLength(75)]
    [Required()]
    public string CreUsr { get; set; }

    /// <summary>
    /// Host name creador del registro
    /// </summary>
    [StringLength(75)]
    [Required()]
    public string CreHsn { get; set; }

    /// <summary>
    /// # de proceso creador del registro
    /// </summary>
    [StringLength(10)]
    [Required()]
    public string CreHid { get; set; }

    /// <summary>
    /// IP creador del registro
    /// </summary>
    [StringLength(48)]
    [Required()]
    public string CreIps { get; set; }

    /// <summary>
    /// Fecha de modificación del registro
    /// </summary>
    [Required()]
    public DateTime ModFch { get; set; }

    /// <summary>
    /// Usuario modificador del registro
    /// </summary>
    [StringLength(75)]
    [Required()]
    public string ModUsr { get; set; }

    /// <summary>
    /// Host name modificador del registro
    /// </summary>
    [StringLength(75)]
    [Required()]
    public string ModHsn { get; set; }

    /// <summary>
    /// # de proceso modificador del registro
    /// </summary>
    [StringLength(10)]
    [Required()]
    public string ModHid { get; set; }

    /// <summary>
    /// IP modificador del registro
    /// </summary>
    [StringLength(48)]
    [Required()]
    public string ModIps { get; set; }

    //public virtual estadoscnt estadoscnt { get; set; }

    //public virtual modulosdocumentos modulosdocumentos { get; set; }

    //public virtual IList<asientoscontablesdetalle> asientoscontablesdetalles { get; set; }

    //public virtual IList<asientoscontableshistorial> asientoscontableshistorials { get; set; }

    #region Extensibility Method Definitions

    partial void OnCreated();

    #endregion
}
