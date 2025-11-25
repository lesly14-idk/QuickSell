namespace QuicksellAPI.Models;

public class Producto
{
    public int id_producto { get; set; }
    public int id_proveedor { get; set; }
    public int id_categoria { get; set; }
    public string nombre_producto { get; set; } = string.Empty;
    public string descripcion_producto { get; set; } = string.Empty;
    public decimal precio_producto { get; set; }
    public int stock_producto { get; set; }
    public string estado_creacion { get; set; } = string.Empty;
    public DateTime fecha_creacion { get; set; }
    public string validado_por_admin { get; set; } = "FALSE";

    // Relaciones
    public Proveedor? Proveedor { get; set; }
    public Categoria? Categoria { get; set; }
    public ICollection<DetalleSolicitud>? DetallesSolicitud { get; set; }
}