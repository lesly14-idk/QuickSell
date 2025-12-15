namespace QuicksellAPI.DTOs;

public class SolicitudCompraCreateDto
{
    public int id_usuario { get; set; }
    public int id_proveedor { get; set; }
    public decimal total { get; set; }
    public string estado { get; set; } = "Pendiente"; // Pendiente, Procesando, rechazada, enviada, entregada, cancelada
    public string nota_seguimiento { get; set; } = string.Empty;
}