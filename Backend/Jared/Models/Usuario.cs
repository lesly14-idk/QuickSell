namespace QuicksellAPI.Models;

public class Usuario
{
    public int id_usuario { get; set; }
    public int id_tipoUsuario { get; set; }  // ← CAMBIO: ahora es FK
    public string nombre_usuario { get; set; } = string.Empty;
    public string telefono_usuario { get; set; } = string.Empty;
    public string direccion_usuario { get; set; } = string.Empty;
    public string email_usuario { get; set; } = string.Empty;
    public string contraseña_usuario { get; set; } = string.Empty;
    public string estado_usuario { get; set; } = string.Empty;
    public DateTime fecha_registro { get; set; }

    // Relaciones
    public TipoUsuario? TipoUsuario { get; set; }  // ← NUEVO
    public ICollection<Proveedor>? Proveedores { get; set; }
    public ICollection<SolicitudDeCompra>? SolicitudesDeCompra { get; set; }
    public ICollection<ConfiguracionesSistema>? Configuraciones { get; set; }
}