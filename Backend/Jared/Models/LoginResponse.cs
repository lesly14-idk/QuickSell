namespace QuicksellAPI.Models;

public class LoginResponse
{
    public string token { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string nombre { get; set; } = string.Empty;
    public string tipo_usuario { get; set; } = string.Empty;
    public int id_usuario { get; set; }
}