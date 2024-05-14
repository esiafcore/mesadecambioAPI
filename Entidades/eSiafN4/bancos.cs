
using System.ComponentModel.DataAnnotations;

namespace eSiafApiN4.Entidades.eSiafN4
{
    /// <summary>
    /// Bancos
    /// </summary>
    public partial class Bancos
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
        /// Código
        /// </summary>
        [StringLength(10)]
        [Required()]
        public string Codigo { get; set; }

        /// <summary>
        /// Descripción
        /// </summary>
        [StringLength(50)]
        [Required()]
        public string Descripci { get; set; }

        /// <summary>
        /// Indicador - Emisor de tarjeta de crédito
        /// </summary>
        [Required()]
        public bool IndTarjetaCredito { get; set; }

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

        [StringLength(50)]
        public string CodigoOperacionSwitch { get; set; }

        public Guid? CuentaContableInterfazSwitch { get; set; }

        [StringLength(50)]
        public string CodigoOperacionMantenimiento { get; set; }

        public Guid? CuentaContableInterfazMantenimiento { get; set; }

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

        /// <summary>
        /// Comisión bancaria por tarjeta de crédito
        /// </summary>
        [Required()]
        public decimal ComisionBancariaPor { get; set; }

        //public virtual estadosbco estadosbco { get; set; }

        //public virtual IList<cuentasbancarias> cuentasbancarias { get; set; }

        //public virtual IList<TransaccionesBco> transaccionesbcos { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
