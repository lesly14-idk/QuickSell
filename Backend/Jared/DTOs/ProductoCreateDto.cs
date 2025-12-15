namespace QuicksellAPI.DTOs;

public class ProductoCreateDto
{
    public int id_proveedor { get; set; }
    public int id_categoria { get; set; }
    public string nombre_producto { get; set; } = string.Empty;
    public string descripcion_producto { get; set; } = string.Empty;
    public decimal precio_producto { get; set; }
    public int stock_producto { get; set; }
    public string estado_creacion { get; set; } = "Disponible"; // Disponible, Agotado, Inactivo
    public string validado_por_admin { get; set; } = "FALSE";
}