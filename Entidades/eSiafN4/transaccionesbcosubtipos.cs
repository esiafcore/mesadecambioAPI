using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class TransaccionesBcoSubtipos {

        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        [Required()]
        public Guid UidCia { get; set; }

        [Required()]
        public Guid UidRegistPad { get; set; }

        [Required()]
        public short Numero { get; set; }

        [StringLength(10)]
        [Required()]
        public string Codigo { get; set; }

        [StringLength(5)]
        [Required()]
        public string Abreviatura { get; set; }

        [Required()]
        public short Factor { get; set; }

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

        //public virtual IList<TransaccionesBco> transaccionesbcos { get; set; }

        //public virtual TransaccionesBcoTipos transaccionesbcotipos { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
