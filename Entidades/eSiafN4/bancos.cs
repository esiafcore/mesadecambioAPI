
using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class Bancos
    {
        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        [Required()]
        public Guid UidCia { get; set; }

        [StringLength(10)]
        [Required()]
        public string Codigo { get; set; }

        [StringLength(50)]
        [Required()]
        public string Descripci { get; set; }

        [Required()]
        public bool IndTarjetaCredito { get; set; }

        [Required()]
        public int NumeroObjeto { get; set; }

        [Required()]
        public int NumeroEstado { get; set; }

        [StringLength(50)]
        public string CodigoOperacionSwitch { get; set; }

        public Guid? CuentaContableInterfazSwitch { get; set; }

        [StringLength(50)]
        public string CodigoOperacionMantenimiento { get; set; }

        public Guid? CuentaContableInterfazMantenimiento { get; set; }

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
