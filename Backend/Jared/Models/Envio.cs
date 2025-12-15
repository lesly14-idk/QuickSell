namespace QuicksellAPI.Models;

public class Envio
{
    public int id_envios { get; set; }
    public int id_solicitud { get; set; }
    public string tipo_envio { get; set; } = string.Empty;
    public string direccion_entrega { get; set; } = string.Empty;
    public string numero_guia { get; set; } = string.Empty;
    public string estado_envio { get; set; } = string.Empty;
    public DateTime fecha_actualizacion { get; set; }

    // Relaciones
    public SolicitudDeCompra? SolicitudDeCompra { get; set; }
}