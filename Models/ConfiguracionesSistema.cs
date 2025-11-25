namespace QuicksellAPI.Models;

public class ConfiguracionesSistema
{
    public int id_config { get; set; }
    public string nombre_config { get; set; } = string.Empty;
    public string valor { get; set; } = string.Empty;
    public DateTime ultima_modificacion { get; set; }
    public string modificado_por { get; set; } = string.Empty;
    public int id_usuario { get; set; }

    // Relaciones
    public Usuario? Usuario { get; set; }
}