namespace QuicksellAPI.Models;

public class LoginRequest
{
    public string email { get; set; } = string.Empty;
    public string contraseña { get; set; } = string.Empty;
}