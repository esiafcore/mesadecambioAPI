using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class TransaccionesBcoTipos {

        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        [Required()]
        public Guid UidCia { get; set; }

        [Required()]
        public short Numero { get; set; }

        [StringLength(10)]
        [Required()]
        public string Codigo { get; set; }

        [StringLength(75)]
        [Required()]
        public string Descripci { get; set; }

        [StringLength(75)]
        [Required()]
        public string DescripciFor { get; set; }

        [Required()]
        public long Contador { get; set; }

        [Required()]
        public long ContadorTemporal { get; set; }

        [StringLength(15)]
        public string FormatoContador { get; set; }

        [StringLength(15)]
        [Required()]
        public string FormatoContadorTemporal { get; set; }

        [Required()]
        public short ContadorPaddingIzquierdo { get; set; }

        [Required()]
        public short ContadorTemporalPaddingIzquierdo { get; set; }

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
