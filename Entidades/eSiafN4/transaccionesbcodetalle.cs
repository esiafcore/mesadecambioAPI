﻿using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4
{
    public partial class TransaccionesBcoDetalle {

        [Key]
        [Required()]
        public Guid UidRegist { get; set; }

        [Required()]
        public Guid UidCia { get; set; }

        [Required()]
        public Guid UidRegistPad { get; set; }

        [Required()]
        public Guid UidCuentaContable { get; set; }

        public Guid? UidCentroCostoContable { get; set; }

        public Guid? UidAuxiliarContable { get; set; }

        public Guid? UidCuentaPresupuesto { get; set; }

        public Guid? UidCentroCostoPresupuesto { get; set; }

        public Guid? UidAuxiliarPresupuesto { get; set; }

        public Guid? ProyectoActividadUid { get; set; }

        public Guid? UidDocumento { get; set; }

        [StringLength(10)]
        public string CodigoDocumento { get; set; }

        [Required()]
        public int NumeroLinea { get; set; }

        [Required()]
        public short TipoMovimiento { get; set; }

        [Required()]
        public decimal TipoCambioMonfor { get; set; }

        [Required()]
        public decimal TipoCambioMonxtr { get; set; }

        [Required()]
        public decimal TipoCambioParaMonfor { get; set; }

        [Required()]
        public decimal TipoCambioParaMonxtr { get; set; }

        [Required()]
        public decimal MontoMonbas { get; set; }

        [Required()]
        public decimal MontoMonfor { get; set; }

        [Required()]
        public decimal MontoMonxtr { get; set; }

        /// <summary>
        /// Uid beneficiario
        /// </summary>
        public Guid? UidBeneficiario { get; set; }

        /// <summary>
        /// Uid entidad
        /// </summary>
        public Guid? UidEntidad { get; set; }

        /// <summary>
        /// Tipo de beneficiario
        /// </summary>
        public short? TipoBeneficiario { get; set; }

        /// <summary>
        /// Indicador - Partida del diferencial
        /// </summary>
        [Required()]
        public bool IndDiferencial { get; set; }

        /// <summary>
        /// Comentario de la línea
        /// </summary>
        [StringLength(250)]
        public string Comentarios { get; set; }

        /// <summary>
        /// Tipo de registro
        /// </summary>
        public short? TipoRegistro { get; set; }

        /// <summary>
        /// Indicador Partida de Cuadratura contable del asiento
        /// </summary>
        [Required()]
        public bool InddeCuadratura { get; set; }

        [Required()]
        public short NumeroTipoCambio { get; set; }

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

        //public virtual TransaccionesBco transaccionesbco { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
