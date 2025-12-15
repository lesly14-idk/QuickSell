namespace QuicksellAPI.Models;

public class HistorialDeOperaciones
{
    public int id_operaciones { get; set; }
    public string tipo_operacion { get; set; } = string.Empty;
    public string tabla_afectada { get; set; } = string.Empty;
    public string descripcion { get; set; } = string.Empty;
    public DateTime fecha_operacion { get; set; }
    
    // Ya NO tiene relación con Usuario
}