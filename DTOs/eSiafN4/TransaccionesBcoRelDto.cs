namespace XanesN8.Api.DTOs.eSiafN4;

public class TransaccionesBcoRelDto
{
    public Guid UidRegistro { get; set; }
    public Guid UidCia { get; set; }
    public Guid UidTransaccionBco { get; set; }
    public Guid UidTransaccionesRelacionada { get; set; }
    public short TipoRelacion { get; set; }
    public decimal MontoTransaccionBancaria { get; set; }
    public decimal MontoTransaccionRelacionada { get; set; }
    public decimal MontoTransaccionBancariaDisponible { get; set; }
    public short Consecutivo { get; set; }
    public DateTime CreFch { get; set; }
    public string CreUsr { get; set; }
    public string CreHsn { get; set; }
    public string CreHid { get; set; }
    public string CreIps { get; set; }
    public DateTime ModFch { get; set; }
    public string ModUsr { get; set; }
    public string ModHsn { get; set; }
    public string ModHid { get; set; }
    public string ModIps { get; set; }
    public short NumeroMoneda { get; set; }
    public Guid UidTransaccionesRelacionadaPadre { get; set; }
}