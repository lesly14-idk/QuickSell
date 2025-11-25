namespace QuicksellAPI.Models;

public class SolicitudDeCompra
{
    public int id_solicitud { get; set; }
    public int id_usuario { get; set; }
    public int id_proveedor { get; set; }
    public DateTime fecha_solicitud { get; set; }
    public decimal total { get; set; }
    public string estado { get; set; } = string.Empty;
    public string gestionado_por_admin { get; set; } = "FALSE";
    public string nota_seguimiento { get; set; } = string.Empty;

    // Relaciones
    public Usuario? Usuario { get; set; }
    public Proveedor? Proveedor { get; set; }
    public ICollection<DetalleSolicitud>? DetallesSolicitud { get; set; }
    public ICollection<Pago>? Pagos { get; set; }
    public ICollection<Envio>? Envios { get; set; }
}