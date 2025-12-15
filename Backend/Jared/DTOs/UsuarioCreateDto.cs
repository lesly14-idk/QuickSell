namespace QuicksellAPI.DTOs;

public class UsuarioCreateDto
{
    public string nombre_usuario { get; set; } = string.Empty;
    public string telefono_usuario { get; set; } = string.Empty;
    public string direccion_usuario { get; set; } = string.Empty;
    public string email_usuario { get; set; } = string.Empty;
    public string contraseña_usuario { get; set; } = string.Empty;
    public string tipo_usuario { get; set; } = "Cliente"; // Cliente, Proveedor, Admin
    public string estado_usuario { get; set; } = "Activo"; // Activo, Inactivo, Suspendido
}