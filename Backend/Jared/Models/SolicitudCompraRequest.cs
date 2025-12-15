namespace QuicksellAPI.Models;

public class SolicitudCompraRequest
{
    public int id_usuario { get; set; }
    public int id_proveedor { get; set; }
    public string nota_seguimiento { get; set; } = string.Empty;
    
    // Lista de productos que se van a comprar
    public List<DetalleCompraItem> detalles { get; set; } = new();
}

public class DetalleCompraItem
{
    public int id_producto { get; set; }
    public int cantidad { get; set; }
}