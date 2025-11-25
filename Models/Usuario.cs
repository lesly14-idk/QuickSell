namespace QuicksellAPI.Models;

public class Usuario
{
    public int id_usuario { get; set; }
    public string nombre_usuario { get; set; } = string.Empty;
    public string telefono_usuario { get; set; } = string.Empty;
    public string direccion_usuario { get; set; } = string.Empty;
    public string email_usuario { get; set; } = string.Empty;
    public string contraseña_usuario { get; set; } = string.Empty;
    public string tipo_usuario { get; set; } = string.Empty;
    public string estado_usuario { get; set; } = string.Empty;
    public DateTime fecha_registro { get; set; }

    // Relaciones
    public ICollection<Proveedor>? Proveedores { get; set; }
    public ICollection<SolicitudDeCompra>? SolicitudesDeCompra { get; set; }
    public ICollection<HistorialDeOperaciones>? HistorialOperaciones { get; set; }
    public ICollection<ConfiguracionesSistema>? Configuraciones { get; set; }
}