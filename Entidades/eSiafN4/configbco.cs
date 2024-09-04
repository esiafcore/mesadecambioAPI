namespace XanesN8.Api.Entidades.eSiafN4
{
    /// <summary>
    /// Configuración Banco
    /// </summary>
    public partial class ConfigBco {

        /// <summary>
        /// Uid cía
        /// </summary>
        public Guid UidCia { get; set; }

        /// <summary>
        /// Uid cuenta contable de diferencias positivas en conciliación bancaria
        /// </summary>
        public Guid? CuentaContableDifPositivaConciliacion { get; set; }

        /// <summary>
        /// Uid cuenta contable de diferencias negativas en conciliación bancaria
        /// </summary>
        public Guid? CuentaContableDifNegativaConciliacion { get; set; }

        /// <summary>
        /// Uid cuenta contable de interfaz de banco
        /// </summary>
        public Guid? CuentacontableInterfaz { get; set; }

        /// <summary>
        /// Cuenta contable de carga inicial
        /// </summary>
        public Guid? CuentaContableCargaInicial { get; set; }

        /// <summary>
        /// Cuenta contable depósito no comprensado
        /// </summary>
        public Guid? CuentaContableDepositoNoCompensado { get; set; }

        /// <summary>
        /// Cuenta contable depósito contrapartida
        /// </summary>
        public Guid? CuentaContableDepositoContraPartida { get; set; }

        /// <summary>
        /// Uid cuenta contable de gastos por comisión de transferencia bancaría
        /// </summary>
        public Guid? CuentaContableComisionTransferencia { get; set; }

        /// <summary>
        /// Consecutivo de transacciones por:
        /// </summary>
        public byte ConsecutivoTransaPor { get; set; }

        /// <summary>
        /// # de decimales a usar en transacciones de banco
        /// </summary>
        public byte DecimalesTransacion { get; set; }

        /// <summary>
        /// # de decimales a usar en saldos
        /// </summary>
        public byte DecimalesSaldo { get; set; }

        public byte DecimalesTipoCambio { get; set; }

        /// <summary>
        /// Uid proveedor de única vez
        /// </summary>
        public Guid? ProveedorUnicaVez { get; set; }

        /// <summary>
        /// Integrado con contabilidad
        /// </summary>
        public bool IndUsaModuloContabilidad { get; set; }

        /// <summary>
        /// Indicador Impresión de Cheques en dos fases
        /// </summary>
        public bool IndImpresionDosFases { get; set; }

        /// <summary>
        /// Indicador - Guardar al presionar el botón imprimir
        /// </summary>
        public bool IndImprimiralGuardar { get; set; }

        /// <summary>
        /// Indicador Contabilizar al imprimir
        /// </summary>
        public bool IndContabilizaralImprimir { get; set; }

        /// <summary>
        /// Indicador imprimir cheques
        /// </summary>
        public bool? IndImprimirCheques { get; set; }

        /// <summary>
        /// Indicador - Generar contrapartida contable deposito
        /// </summary>
        public bool IndGenerarContraPartidaContableDeposito { get; set; }

        /// <summary>
        /// Mostrar nombre de ciudad en cheque
        /// </summary>
        public bool IndMostrarCiudad { get; set; }

        /// <summary>
        /// # de formato fecha
        /// </summary>
        public bool IndUsarFormatoFechaCorto { get; set; }

        /// <summary>
        /// Indicador Generar nota de debito por comisión transferencia bancaria.
        /// </summary>
        public bool IndGenerarNdComisionTransferencia { get; set; }

        public bool IndUsaSolicituddePago { get; set; }

        public bool IndConsecutivoSolicitudPagoIncluyeMes { get; set; }

        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime CreFch { get; set; }

        /// <summary>
        /// Usuario creador del registro
        /// </summary>
        public string CreUsr { get; set; }

        /// <summary>
        /// Host name creador del registro
        /// </summary>
        public string CreHsn { get; set; }

        /// <summary>
        /// # de proceso creador del registro
        /// </summary>
        public string CreHid { get; set; }

        /// <summary>
        /// IP creador del registro
        /// </summary>
        public string CreIps { get; set; }

        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime ModFch { get; set; }

        /// <summary>
        /// Usuario modificador del registro
        /// </summary>
        public string ModUsr { get; set; }

        /// <summary>
        /// Host name modificador del registro
        /// </summary>
        public string ModHsn { get; set; }

        /// <summary>
        /// # de proceso modificador del registro
        /// </summary>
        public string ModHid { get; set; }

        /// <summary>
        /// IP modificador del registro
        /// </summary>
        public string ModIps { get; set; }

        public short? VersionFormatoImpresion { get; set; }

    }

}
