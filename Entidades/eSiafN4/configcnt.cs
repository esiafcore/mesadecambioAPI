
using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class ConfigCnt {

        [Key]
        [Required()]
        public Guid UidCia { get; set; }

        [Required()]
        public bool IndContabilizarDirecto { get; set; }

        [Required()]
        public bool IndCentroCosto { get; set; }

        [Required()]
        public bool IndIncluirDocumentoDetalle { get; set; }

        [Required()]
        public bool IndConsecutivoAsiento { get; set; }

        [Required()]
        public short CuentaContableSegmentos { get; set; }

        [Required()]
        public short CuentaContableDigitos { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento01 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento02 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento03 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento04 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento05 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento06 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento07 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento08 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento09 { get; set; }

        [Required()]
        public short CuentaContableDigitosSegmento10 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento01 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento02 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento03 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento04 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento05 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento06 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento07 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento08 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento09 { get; set; }

        [StringLength(15)]
        public string CuentaContablePatronSegmento10 { get; set; }

        [StringLength(250)]
        public string CuentaContableExpresionRegular { get; set; }

        [StringLength(250)]
        public string CuentaContableExpresionFormateo { get; set; }

        [StringLength(250)]
        public string CuentaContableExpresionMascara { get; set; }

        [Required()]
        public short CentroCostoSegmentos { get; set; }

        [Required()]
        public short CentroCostoDigitos { get; set; }

        [Required()]
        public short CentroCostoDigitosSegmento01 { get; set; }

        [Required()]
        public short CentroCostoDigitosSegmento02 { get; set; }

        [Required()]
        public short CentroCostoDigitosSegmento03 { get; set; }

        [Required()]
        public short CentroCostoDigitosSegmento04 { get; set; }

        [Required()]
        public short CentroCostoDigitosSegmento05 { get; set; }

        [StringLength(15)]
        public string CentroCostoPatronSegmento01 { get; set; }

        [StringLength(15)]
        public string CentroCostoPatronSegmento02 { get; set; }

        [StringLength(15)]
        public string CentroCostoPatronSegmento03 { get; set; }

        [StringLength(15)]
        public string CentroCostoPatronSegmento04 { get; set; }

        [StringLength(15)]
        public string CentroCostoPatronSegmento05 { get; set; }

        [StringLength(250)]
        public string CentroCostoExpresionRegular { get; set; }

        [StringLength(250)]
        public string CentroCostoExpresionFormateo { get; set; }

        [StringLength(250)]
        public string CentroCostoExpresionMascara { get; set; }

        [Required()]
        public bool IndCalcularImpuestoRenta { get; set; }

        [Required()]
        public decimal PorcentajeImpuestoRenta { get; set; }

        public Guid? ProyectoCompanyUid { get; set; }

        [Required()]
        public decimal MontoMaximoDiferenciaContable { get; set; }

        [Required()]
        public bool IndUsaProyecto { get; set; }

        [Required()]
        public bool IndUsaSucursal { get; set; }

        [Required()]
        public bool IndUsarAuxiliarContable { get; set; }

        public Guid? CuentaContableCierrePeriodoAnterior { get; set; }

        public Guid? CuentaContableCierreMesActual { get; set; }

        public Guid? CuentaContableCierreHastaMesAnterior { get; set; }

        public Guid? CuentaContableGananciaDiferencial { get; set; }

        public Guid? CuentaContablePerdidaDiferencial { get; set; }

        public Guid? CentroCostoPerdidaDiferencialUid { get; set; }

        public Guid? CentroCostoGananciaDiferencialUid { get; set; }

        [StringLength(1)]
        public string SeparadorNivel { get; set; }

        [Required()]
        public short NumeroFormatoImpresionComprobante { get; set; }

        [Required()]
        public byte CharacterChasingDescripcion { get; set; }

        [Required()]
        public short DecimalTransaccion { get; set; }

        [Required()]
        public short DecimalSaldos { get; set; }

        [Required()]
        public short DecimalTipoCambio { get; set; }

        [Required()]
        public byte ConsecutivoAsientopor { get; set; }

        public Guid? CuentaContableCargaInicial { get; set; }

        [Required()]
        public DateTime CreFch { get; set; }

        [StringLength(75)]
        [Required()]
        public string CreUsr { get; set; }

        [StringLength(75)]
        [Required()]
        public string CreHsn { get; set; }

        [StringLength(10)]
        [Required()]
        public string CreHid { get; set; }

        [StringLength(48)]
        [Required()]
        public string CreIps { get; set; }

        [Required()]
        public DateTime ModFch { get; set; }

        [StringLength(75)]
        [Required()]
        public string ModUsr { get; set; }

        [StringLength(75)]
        [Required()]
        public string ModHsn { get; set; }

        [StringLength(10)]
        [Required()]
        public string ModHid { get; set; }

        [StringLength(48)]
        [Required()]
        public string ModIps { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
