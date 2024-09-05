using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    /// <summary>
    /// Transacciones de banco&gt;Tipos
    /// </summary>
    public partial class TransaccionesBcoTipos {

        /// <summary>
        /// UID registro
        /// </summary>
        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        /// <summary>
        /// UID cia
        /// </summary>
        [Required()]
        public Guid UidCia { get; set; }

        /// <summary>
        /// # del registro
        /// </summary>
        [Required()]
        public short Numero { get; set; }

        /// <summary>
        /// Código del registro
        /// </summary>
        [StringLength(10)]
        [Required()]
        public string Codigo { get; set; }

        /// <summary>
        /// Descripción
        /// </summary>
        [StringLength(75)]
        [Required()]
        public string Descripci { get; set; }

        /// <summary>
        /// Descripción foránea
        /// </summary>
        [StringLength(75)]
        [Required()]
        public string DescripciFor { get; set; }

        /// <summary>
        /// # consecutivo
        /// </summary>
        [Required()]
        public long Contador { get; set; }

        /// <summary>
        /// # consecutivo temporal
        /// </summary>
        [Required()]
        public long ContadorTemporal { get; set; }

        /// <summary>
        /// Formato del contador
        /// </summary>
        [StringLength(15)]
        public string FormatoContador { get; set; }

        /// <summary>
        /// Formato contador temporal
        /// </summary>
        [StringLength(15)]
        [Required()]
        public string FormatoContadorTemporal { get; set; }

        /// <summary>
        /// Contador padding izquierdo
        /// </summary>
        [Required()]
        public short ContadorPaddingIzquierdo { get; set; }

        /// <summary>
        /// Contador temporal padding izquierdo
        /// </summary>
        [Required()]
        public short ContadorTemporalPaddingIzquierdo { get; set; }

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

        //public virtual IList<TransaccionesBco> transaccionesbcos { get; set; }

        //public virtual IList<TransaccionesBcoSubtipos> transaccionesbcosubtipos { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
