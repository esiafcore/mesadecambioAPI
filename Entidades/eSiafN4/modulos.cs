using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class Modulos {

        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        [Required()]
        public Guid UidCia { get; set; }

        [StringLength(5)]
        [Required()]
        public string Codigo { get; set; }

        [Required()]
        public int Numero { get; set; }

        [StringLength(75)]
        [Required()]
        public string Descripci { get; set; }

        [Required()]
        public long ConsecutivoTransa { get; set; }

        [Required()]
        public long ConsecutivoTransaTemporal { get; set; }

        [Required()]
        public long ConsecutivoConta { get; set; }

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

        [StringLength(75)]
        [Required()]
        public string DescripciFor { get; set; }

        //public virtual IList<ModulosDocumentos> modulosdocumentos { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
