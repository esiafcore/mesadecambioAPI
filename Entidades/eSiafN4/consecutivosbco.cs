using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class ConsecutivosBco {

        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        [Required()]
        public Guid UidCia { get; set; }

        [StringLength(5)]
        [Required()]
        public string Categoria { get; set; }

        [StringLength(10)]
        [Required()]
        public string Codigo { get; set; }

        [StringLength(150)]
        [Required()]
        public string NombreCampo { get; set; }

        [Required()]
        public long Contador { get; set; }

        [Required()]
        public long ContadorTemporal { get; set; }

        [StringLength(15)]
        [Required()]
        public string FormatoContador { get; set; }

        [StringLength(15)]
        [Required()]
        public string FormatoContadorTemporal { get; set; }

        [Required()]
        public short ContadorPaddingIzquierdo { get; set; }

        [Required()]
        public short ContadorTemporalPaddingIzquierdo { get; set; }

        public bool? IndAplicar { get; set; }

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

    }

}
