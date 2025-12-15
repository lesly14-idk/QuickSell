namespace QuicksellAPI.Models;

public class TipoUsuario
{
    public int id_tipoUsuario { get; set; }
    public string tipo_nombreUsuario { get; set; } = string.Empty;

    // Relaciones
    public ICollection<Usuario>? Usuarios { get; set; }
}