namespace XanesN8.Api.DTOs.eSiafN4;

public class TransaccionesBcoRelDtoCreate
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
    public short NumeroMoneda { get; set; }
    public Guid UidTransaccionesRelacionadaPadre { get; set; }
}