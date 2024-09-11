
using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4;
public partial class AsientosContablesDetalle
{

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

    public Guid? UidActividadProyecto { get; set; }

    public Guid? UidAuxiliarPresupuesto { get; set; }

    public Guid? UidDocumento { get; set; }

    [StringLength(50)]
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
    public decimal TipoCambioMonParaMonxtr { get; set; }

    [Required()]
    public decimal MontoMonbas { get; set; }

    [Required()]
    public decimal MontoMonfor { get; set; }

    [Required()]
    public decimal MontoMonxtr { get; set; }

    public Guid? UidBeneficiario { get; set; }

    public Guid? UidEntidad { get; set; }

    public short? TipoBeneficiario { get; set; }

    public Guid? UidCuentaBanco { get; set; }

    public Guid? UidArticulo { get; set; }

    public Guid? UidActivoFjio { get; set; }

    public Guid? UidSucursal { get; set; }

    public Guid? UidProyecto { get; set; }

    [Required()]
    public bool IndDiferencial { get; set; }

    [StringLength(250)]
    public string Comentarios { get; set; }

    [Required()]
    public bool InddeCuadratura { get; set; }

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

    //public virtual AsientosContables asientoscontables { get; set; }

    #region Extensibility Method Definitions

    partial void OnCreated();

    #endregion
}


