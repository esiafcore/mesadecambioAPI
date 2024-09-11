using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class ConsecutivosCntDetalle {

        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        [Required()]
        public Guid UidCia { get; set; }

        /// <summary>
        /// Categoria del consecutivo
        /// </summary>
        [StringLength(5)]
        [Required()]
        public string Categoria { get; set; }

        /// <summary>
        /// codigo del tipo de transacción
        /// </summary>
        [StringLength(10)]
        [Required()]
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del campo
        /// </summary>
        [StringLength(150)]
        [Required()]
        public string NombreCampo { get; set; }

        /// <summary>
        /// Contador
        /// </summary>
        [Required()]
        public long Contador { get; set; }

        /// <summary>
        /// Contador temporal
        /// </summary>
        [Required()]
        public long ContadorTemporal { get; set; }

        /// <summary>
        /// Formato contador
        /// </summary>
        [StringLength(15)]
        [Required()]
        public string FormatoContador { get; set; }

        /// <summary>
        /// Formato contador temporal
        /// </summary>
        [StringLength(15)]
        [Required()]
        public string FormatoContadorTemporal { get; set; }

        /// <summary>
        /// # de ceros del padding izquierdo contador
        /// </summary>
        [Required()]
        public short ContadorPaddingIzquierdo { get; set; }

        /// <summary>
        /// # de ceros del padding izquierdo contador temporal
        /// </summary>
        [Required()]
        public short ContadorTemporalPaddingIzquierdo { get; set; }

        /// <summary>
        /// Año Fiscal
        /// </summary>
        [Required()]
        public short YearFiscal { get; set; }

        /// <summary>
        /// Mes fiscal
        /// </summary>
        [Required()]
        public short MesFiscal { get; set; }

        /// <summary>
        /// Indicador de registro en uso
        /// </summary>
        public bool? IndAplicar { get; set; }

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

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
