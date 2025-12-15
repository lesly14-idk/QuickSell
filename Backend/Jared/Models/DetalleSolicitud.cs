namespace QuicksellAPI.Models;

public class DetalleSolicitud
{
    public int id_detalle { get; set; }
    public int id_solicitud { get; set; }
    public int id_producto { get; set; }
    public int cantidad { get; set; }
    public decimal subtotal { get; set; }

    // Relaciones
    public SolicitudDeCompra? SolicitudDeCompra { get; set; }
    public Producto? Producto { get; set; }
}