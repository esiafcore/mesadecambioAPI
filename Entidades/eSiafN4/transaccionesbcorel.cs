using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4;
public partial class TransaccionesBcoRel
{

    [Key]
    [Required()]
    public Guid UidRegistro { get; set; }

    [Required()]
    public Guid UidCia { get; set; }

    [Required()]
    public Guid UidTransaccionBco { get; set; }

    [Required()]
    public Guid UidTransaccionesRelacionada { get; set; }

    [Required()]
    public short TipoRelacion { get; set; }

    [Required()]
    public decimal MontoTransaccionBancaria { get; set; }

    [Required()]
    public decimal MontoTransaccionRelacionada { get; set; }

    [Required()]
    public decimal MontoTransaccionBancariaDisponible { get; set; }

    [Required()]
    public short Consecutivo { get; set; }

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
    public short NumeroMoneda { get; set; }

    [Required()]
    public Guid UidTransaccionesRelacionadaPadre { get; set; }

    //public virtual TransaccionesBco transaccionesbco { get; set; }

    #region Extensibility Method Definitions

    partial void OnCreated();

    #endregion
}
