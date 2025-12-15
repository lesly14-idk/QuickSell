namespace QuicksellAPI.DTOs;

public class ProveedorCreateDto
{
    public int id_usuario { get; set; }
    public string nombre_empresa { get; set; } = string.Empty;
    public string telefono { get; set; } = string.Empty;
    public string direccion { get; set; } = string.Empty;
    public string catalogo_activo { get; set; } = "FALSE"; // TRUE o FALSE
    public string validado_por_proveedor { get; set; } = "FALSE";
    public string validado_por_admin { get; set; } = "FALSE";
}