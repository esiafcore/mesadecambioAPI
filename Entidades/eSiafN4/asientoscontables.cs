using System.ComponentModel.DataAnnotations;

namespace XanesN8.Api.Entidades.eSiafN4;
public partial class AsientosContables
{
    [Key]
    [Required()]
    public Guid UidRegist { get; set; }

    [Required()]
    public Guid UidCia { get; set; }

    [Required()]
    public Guid UidModulo { get; set; }

    [Required()]
    public Guid UidModuloDocumento { get; set; }

    [Required()]
    public short NumeroMoneda { get; set; }

    [Required()]
    public int NumeroEstado { get; set; }

    [Required()]
    public int NumeroObjeto { get; set; }

    [Required()]
    public short YearFiscal { get; set; }

    [Required()]
    public short MesFiscal { get; set; }

    [Required()]
    public DateTime FechaTransa { get; set; }

    [StringLength(30)]
    [Required()]
    public string NumeroTransaccion { get; set; }

    [StringLength(1)]
    [Required()]
    public string SerieInterna { get; set; }

    [StringLength(100)]
    public string NumeroTransaccionRef { get; set; }

    [Required()]
    public decimal TipoCambioMonfor { get; set; }

    [Required()]
    public decimal TipoCambioMonxtr { get; set; }

    [Required()]
    public decimal TipoCambioParaMonfor { get; set; }

    [Required()]
    public decimal TipoCambioParaMonxtr { get; set; }

    public short? NumeroLineas { get; set; }

    [Required()]
    public decimal MontoDebitoMonbas { get; set; }

    [Required()]
    public decimal MontoDebitoMonfor { get; set; }

    [Required()]
    public decimal MontoDebitoMonxtr { get; set; }

    [Required()]
    public decimal MontoCreditoMonbas { get; set; }

    [Required()]
    public decimal MontoCreditoMonfor { get; set; }

    [Required()]
    public decimal MontoCreditoMonxtr { get; set; }

    public Guid? UidProyecto { get; set; }

    public Guid? UidSucursal { get; set; }

    [Required()]
    public string Comentarios { get; set; }

    public DateTime? FechaTransaAnula { get; set; }

    [StringLength(30)]
    public string NumeroTransaccionAnula { get; set; }

    [StringLength(1)]
    public string SerieInternaAnula { get; set; }

    public short? YearFiscalAnula { get; set; }

    public short? MesFiscalAnula { get; set; }

    public Guid? UidRegistRef { get; set; }

    [Required()]
    public bool IndOkay { get; set; }

    public string ComentariosSistema { get; set; }

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

    //public virtual estadoscnt estadoscnt { get; set; }

    //public virtual modulosdocumentos modulosdocumentos { get; set; }

    //public virtual IList<AsientosContablesDetalle> asientoscontablesdetalles { get; set; }

    //public virtual IList<asientoscontableshistorial> asientoscontableshistorials { get; set; }

    #region Extensibility Method Definitions

    partial void OnCreated();

    #endregion
}


