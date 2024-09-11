﻿
using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class TransaccionesBco
    {
        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        [Required()]
        public Guid UidCia { get; set; }

        [Required()]
        public Guid UidBanco { get; set; }

        [Required()]
        public Guid UidCuentaBancaria { get; set; }

        public Guid? UidCuentaBancariaRef { get; set; }

        [Required()]
        public DateTime FechaTransa { get; set; }

        [StringLength(6)]
        [Required()]
        public string YearMonthFiscal { get; set; } = null!;

        [Required()]
        public short YearFiscal { get; set; }

        [Required()]
        public short MesFiscal { get; set; }

        /// <summary>
        /// Uid tipo de transacción
        /// </summary>
        [Required()]
        public Guid UidTipo { get; set; }

        /// <summary>
        /// Uid subtipo de transacción
        /// </summary>
        [Required()]
        public Guid UidSubtipo { get; set; }

        /// <summary>
        /// # de transacción
        /// </summary>
        [StringLength(30)]
        [Required()]
        public string NumeroTransaccion { get; set; } = null!;

        /// <summary>
        /// Serie Interna
        /// </summary>
        [StringLength(1)]
        [Required()]
        public string SerieInterna { get; set; } = null!;

        /// <summary>
        /// # de transacción de referencia
        /// </summary>
        [StringLength(30)]
        public string NumeroTransaccionRef { get; set; } = null!;

        /// <summary>
        /// Uid transacción de referencia. Para cheque es solicitud de pago
        /// </summary>
        public Guid? UidTransaccionRef { get; set; }

        /// <summary>
        /// # de moneda
        /// </summary>
        [Required()]
        public short NumeroMoneda { get; set; }

        /// <summary>
        /// # de líneas del detalle
        /// </summary>
        [Required()]
        public short NumeroLineas { get; set; }

        /// <summary>
        /// Fecha de anulación de la transacción
        /// </summary>
        public DateTime? FechaTransaAnula { get; set; }

        /// <summary>
        /// Año fiscal de anulación
        /// </summary>
        public short? YearFiscalAnula { get; set; }

        /// <summary>
        /// Mes fiscal de anulación
        /// </summary>
        public short? MesFiscalAnula { get; set; }

        /// <summary>
        /// Uid de transacción de anulación
        /// </summary>
        public Guid? UidTransaccionAnula { get; set; }

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
        public decimal TipoCambioParaMonfor { get; set; }

        /// <summary>
        /// Tipo de cambio paralelo moneda foránea adicional
        /// </summary>
        [Required()]
        public decimal TipoCambioParaMonxtr { get; set; }

        /// <summary>
        /// # de objeto
        /// </summary>
        [Required()]
        public int NumeroObjeto { get; set; }

        /// <summary>
        /// # de estado
        /// </summary>
        [Required()]
        public int NumeroEstado { get; set; }

        /// <summary>
        /// Comentarios de la transacción
        /// </summary>
        [StringLength(1000)]
        public string Comentarios { get; set; } = null!;

        /// <summary>
        /// Indicador okay
        /// </summary>
        [Required()]
        public bool IndOkay { get; set; }

        /// <summary>
        /// Monto de comisión moneda foránea adicional
        /// </summary>
        [Required()]
        public decimal MontoComisionMonxtr { get; set; }

        /// <summary>
        /// Monto de comisión moneda foránea
        /// </summary>
        [Required()]
        public decimal MontoComisionMonfor { get; set; }

        /// <summary>
        /// Monto de comisión moneda base
        /// </summary>
        [Required()]
        public decimal MontoComisionMonbas { get; set; }

        /// <summary>
        /// Subtotal neto moneda base
        /// </summary>
        [Required()]
        public decimal SubtotalNetoMonbas { get; set; }

        /// <summary>
        /// Subtotal neto moneda foránea
        /// </summary>
        [Required()]
        public decimal SubtotalNetoMonfor { get; set; }

        /// <summary>
        /// Subtotal neto moneda foránea adicional
        /// </summary>
        [Required()]
        public decimal SubtotalNetoMonxtr { get; set; }

        /// <summary>
        /// Total factura moneda base
        /// </summary>
        [Required()]
        public decimal TotalMonbas { get; set; }

        /// <summary>
        /// Total factura moneda foránea
        /// </summary>
        [Required()]
        public decimal TotalMonfor { get; set; }

        /// <summary>
        /// Total factura moneda foránea adicional
        /// </summary>
        [Required()]
        public decimal TotalMonxtr { get; set; }

        /// <summary>
        /// Retención moneda base
        /// </summary>
        [Required()]
        public decimal RetencionMonbas { get; set; }

        /// <summary>
        /// Retención moneda foránea
        /// </summary>
        [Required()]
        public decimal RetencionMonfor { get; set; }

        /// <summary>
        /// Retención moneda foránea adicional
        /// </summary>
        [Required()]
        public decimal RetencionMonxtr { get; set; }

        /// <summary>
        /// Monto transacción moneda base
        /// </summary>
        [Required()]
        public decimal MontoMonbas { get; set; }

        /// <summary>
        /// Monto transacción moneda foránea
        /// </summary>
        [Required()]
        public decimal MontoMonfor { get; set; }

        /// <summary>
        /// Monto transacción moneda foránea adicional
        /// </summary>
        [Required()]
        public decimal MontoMonxtr { get; set; }

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
        /// Uid Beneficiario
        /// </summary>
        public Guid? UidBeneficiario { get; set; }

        /// <summary>
        /// Uid Entidad
        /// </summary>
        public Guid? UidEntidad { get; set; }

        /// <summary>
        /// Tipo de beneficiario
        /// </summary>
        public short? TipoBeneficiario { get; set; }

        /// <summary>
        /// Uid cuenta contable
        /// </summary>
        public Guid? UidAsientoContable { get; set; }

        /// <summary>
        /// Uid asiento contable de anulación
        /// </summary>
        public Guid? UidAsientoContableAnula { get; set; }

        /// <summary>
        /// Uid sucursal
        /// </summary>
        public Guid? UidSucursal { get; set; }

        public Guid? UidSolicitudPago { get; set; }

        /// <summary>
        /// Uid proyecto
        /// </summary>
        public Guid? UidProyecto { get; set; }

        public Guid? UidDonante { get; set; }

        /// <summary>
        /// Indicador - Transacción es conciliable
        /// </summary>
        [Required()]
        public bool IndConciliable { get; set; }

        /// <summary>
        /// Indicador - Transacción es inicial
        /// </summary>
        [Required()]
        public bool IndTransaccionInicial { get; set; }

        /// <summary>
        /// Monto de ajuste al importe para conciliar
        /// </summary>
        [Required()]
        public decimal MontoAjusteConciMonbas { get; set; }

        /// <summary>
        /// Monto de ajuste al importe para conciliar moneda foránea
        /// </summary>
        [Required()]
        public decimal MontoAjusteConciMonfor { get; set; }

        /// <summary>
        /// Monto de ajuste al importe para conciliar moneda foránea adicional
        /// </summary>
        [Required()]
        public decimal MontoAjusteConciMonxtr { get; set; }

        /// <summary>
        /// Indicador - Transacción conciliada
        /// </summary>
        [Required()]
        public bool IndConciliado { get; set; }

        /// <summary>
        /// Indicador - Transaccion flotante
        /// </summary>
        [Required()]
        public bool IndFlotante { get; set; }

        /// <summary>
        /// Uid conciliación
        /// </summary>
        public Guid? UidConciliacion { get; set; }

        /// <summary>
        /// Indicador de compensado
        /// </summary>
        [Required()]
        public bool IndCompensado { get; set; }

        /// <summary>
        /// Fecha ultima de compensación
        /// </summary>
        public DateTime? FechaCompensacion { get; set; }

        /// <summary>
        /// Indicador de retención
        /// </summary>
        [Required()]
        public bool IndRetencion { get; set; }

        /// <summary>
        /// Uid del pago de caja
        /// </summary>
        public Guid? UidPago { get; set; }

        /// <summary>
        /// Indicador - Impreso el comprobante del cheque
        /// </summary>
        [Required()]
        public bool IndImpresoComprobante { get; set; }

        /// <summary>
        /// Indicador - Impreso el cheque
        /// </summary>
        [Required()]
        public bool IndImpresoCheque { get; set; }

        /// <summary>
        /// imprimir cheque a nombre de
        /// </summary>
        [StringLength(250)]
        public string ImprimirChequeaNombrede { get; set; } = null!;

        /// <summary>
        /// # de identificacion
        /// </summary>
        [StringLength(25)]
        public string NumeroIdentificacion { get; set; } = null!;

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
        public string CreUsr { get; set; } = null!;

        /// <summary>
        /// Host name creador del registro
        /// </summary>
        [StringLength(75)]
        [Required()]
        public string CreHsn { get; set; } = null!;

        /// <summary>
        /// # de proceso creador del registro
        /// </summary>
        [StringLength(10)]
        [Required()]
        public string CreHid { get; set; } = null!;

        /// <summary>
        /// IP creador del registro
        /// </summary>
        [StringLength(48)]
        [Required()]
        public string CreIps { get; set; } = null!;

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
        public string ModUsr { get; set; } = null!;

        /// <summary>
        /// Host name modificador del registro
        /// </summary>
        [StringLength(75)]
        [Required()]
        public string ModHsn { get; set; } = null!;

        /// <summary>
        /// # de proceso modificador del registro
        /// </summary>
        [StringLength(10)]
        [Required()]
        public string ModHid { get; set; } = null!;

        /// <summary>
        /// IP modificador del registro
        /// </summary>
        [StringLength(48)]
        [Required()]
        public string ModIps { get; set; } = null!;

        /// <summary>
        /// Indicador mesa de cambio
        /// </summary>
        [Required()]
        public bool IndMesaDeCambio { get; set; }

        /// <summary>
        /// Transacción de Mesa de Cambio Relacionado
        /// </summary>
        public int? TransaMcRelacionada { get; set; }

        /// <summary>
        /// Transacción de Mesa de Cambio Padre Relacionada
        /// </summary>
        public int? TransaMcRelacionadaParent { get; set; }

        //public virtual Bancos bancos { get; set; }

        //public virtual transaccionesbcotipos transaccionesbcotipos { get; set; }

        //public virtual transaccionesbcosubtipos transaccionesbcosubtipos { get; set; }

        //public virtual cuentasbancarias cuentasbancarias { get; set; }

        //public virtual IList<transaccionesbcodetalle> transaccionesbcodetalles { get; set; }

        //public virtual IList<transaccionesbcohistorial> transaccionesbcohistorials { get; set; }

        //public virtual IList<transaccionesbcorel> transaccionesbcorels { get; set; }

        //public virtual IList<transaccionesbcoretenciones> transaccionesbcoretenciones { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
