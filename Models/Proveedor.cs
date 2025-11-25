namespace QuicksellAPI.Models;

public class Proveedor
{
    public int id_proveedor { get; set; }
    public int id_usuario { get; set; }
    public string nombre_empresa { get; set; } = string.Empty;
    public string telefono { get; set; } = string.Empty;
    public string direccion { get; set; } = string.Empty;
    public string catalogo_activo { get; set; } = "FALSE";
    public string validado_por_proveedor { get; set; } = "FALSE";
    public string validado_por_admin { get; set; } = "FALSE";

    // Relaciones
    public Usuario? Usuario { get; set; }
    public ICollection<Producto>? Productos { get; set; }
    public ICollection<SolicitudDeCompra>? SolicitudesDeCompra { get; set; }
}