namespace QuicksellAPI.DTOs;

public class EnvioCreateDto
{
    public int id_solicitud { get; set; }
    public string tipo_envio { get; set; } = "Local"; // Local, Autogestionado
    public string direccion_entrega { get; set; } = string.Empty;
    public string numero_guia { get; set; } = string.Empty;
    public string estado_envio { get; set; } = "Preparacion"; // Preparacion, En transito, entregado, cancelado
}