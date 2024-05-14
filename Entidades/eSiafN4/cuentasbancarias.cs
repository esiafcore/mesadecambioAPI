using System.ComponentModel.DataAnnotations;

namespace eSiafApiN4.Entidades.eSiafN4
{
    /// <summary>
    /// Cuentas bancarias
    /// </summary>
    public partial class CuentasBancarias
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
        /// Uid cuenta contable
        /// </summary>
        public Guid? UidCuentaContable { get; set; }

        /// <summary>
        /// Uid cuenta bancaria
        /// </summary>
        [Required()]
        public Guid UidBanco { get; set; }

        /// <summary>
        /// # de tipo de cuenta bancaria
        /// </summary>
        [Required()]
        public short NumeroTipo { get; set; }

        /// <summary>
        /// Código de cuenta bancaria
        /// </summary>
        [StringLength(25)]
        [Required()]
        public string Codigo { get; set; } = null!;

        /// <summary>
        /// Descripción
        /// </summary>
        [StringLength(100)]
        [Required()]
        public string Descripci { get; set; } = null!;

        /// <summary>
        /// # de moneda
        /// </summary>
        [Required()]
        public short NumeroMoneda { get; set; }

        /// <summary>
        /// # de consecutivo de cheque
        /// </summary>
        [Required()]
        public long Numero { get; set; }

        /// <summary>
        /// # consecutivo de cheque
        /// </summary>
        [Required()]
        public long Contador { get; set; }

        /// <summary>
        /// # de consecutivo de pagos por transferencia
        /// </summary>
        [Required()]
        public long ContadorTransfer { get; set; }

        /// <summary>
        /// Contador de Pago de Mesa de Cambio
        /// </summary>
        [Required()]
        public long ContadorExchange { get; set; }

        /// <summary>
        /// # consecutivo temporal de cheque
        /// </summary>
        [Required()]
        public long ContadorTemporal { get; set; }

        /// <summary>
        /// # consecutivo temporal de pagos por transferencia
        /// </summary>
        [Required()]
        public long ContadorTemporalTransfer { get; set; }

        /// <summary>
        /// Contador temporal de Pago de Mesa de Cambio
        /// </summary>
        [Required()]
        public long ContadorTemporalExchange { get; set; }

        /// <summary>
        /// Formato contador
        /// </summary>
        [StringLength(15)]
        [Required()]
        public string FormatoContador { get; set; } = null!;

        /// <summary>
        /// Formato contador temporal
        /// </summary>
        [StringLength(15)]
        [Required()]
        public string FormatoContadorTemporal { get; set; } = null!;

        /// <summary>
        /// # de ceros del padding izquierdo contador
        /// </summary>
        [Required()]
        public short ContadorPaddingIzquierdo { get; set; }

        /// <summary>
        /// # de ceros del padding izquierdo contador temporal
        /// </summary>
        [Required()]
        public short ContadorTemporalPaddingIzquierdo { get; set; } = 0;

        /// <summary>
        /// Literal seríal fija
        /// </summary>
        [StringLength(2)]
        [Required()]
        public string LiteralSerial { get; set; } = null!;

        /// <summary>
        /// Literal serial temporal
        /// </summary>
        [StringLength(2)]
        public string LiteralSerialTemporal { get; set; } = null!;

        /// <summary>
        /// Fecha de apertura
        /// </summary>
        [Required()]
        public DateTime FechaApertura { get; set; }

        /// <summary>
        /// Uid formato de impresión
        /// </summary>
        public Guid? UidFormatoImpresion { get; set; }

        /// <summary>
        /// Saldo moneda base
        /// </summary>
        [Required()]
        public decimal SaldoMonbas { get; set; }

        /// <summary>
        /// Saldo moneda foránea
        /// </summary>
        [Required()]
        public decimal SaldoMonfor { get; set; }

        /// <summary>
        /// Saldo moneda foránea adicional
        /// </summary>
        [Required()]
        public decimal SaldoMonxtr { get; set; }

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
        /// Indicador usar en cuentas por cobrar
        /// </summary>
        [Required()]
        public bool IndUsarenCxc { get; set; }

        /// <summary>
        /// Indicador usa forma continua
        /// </summary>
        [Required()]
        public bool IndUsaFormaContinua { get; set; }

        /// <summary>
        /// Indicador - Utiliza impresora matricial
        /// </summary>
        [Required()]
        public bool IndImpresoraMatricial { get; set; }

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

        //public virtual estadosbco estadosbco { get; set; }

        //public virtual Bancos bancos { get; set; }

        //public virtual IList<TransaccionesBco> transaccionesbcos { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
