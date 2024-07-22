namespace XanesN8.Api.DTOs.XanesN8;

public class RespuestaAutenticacionDto
{
    public string Token { get; set; } = null!;
    public DateTime Expiracion { get; set; }
}